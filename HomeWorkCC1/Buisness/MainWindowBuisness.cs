using HomeWorkCC1;
using Prism.Commands;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UI.UserControlls;
using s = ServiceReference1;

namespace UI.Buisness
{
    public class MainWindowBuisness : DependencyObject, INotifyPropertyChanged
    {
        s.Service1Client srv = new s.Service1Client();
        private MainWindow mainWindow;
        public UserLogin userLogin { get; set; }

        /*In order to show grid*/
        private UserDto _userDto;
        public UserDto userDto
        {
            get { return _userDto; }
            set
            {
                _userDto = value;
                OnPropertyChanged(nameof(userDto));
            }
        }
        private ReservationDto _SelectedReservation;
        public ReservationDto SelectedReservation
        {
            get { return _SelectedReservation; }
            set
            {
                _SelectedReservation = value;
                OnPropertyChanged(nameof(SelectedReservation));
            }
        }
        private ObservableCollection<ReservationDto> _reservations;
        public ObservableCollection<ReservationDto> Reservations
        {
            get { return _reservations; }
            set
            {
                _reservations = value;
                OnPropertyChanged(nameof(Reservations));
            }
        }
        /*In order to populate listBox*/


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /*ctor and delegate commands*/
        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand DeleteReservationCommand { get; set; }

        public MainWindowBuisness()   {  }
        public MainWindowBuisness(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.userLogin = new UserLogin();
            this.LoginCommand = new DelegateCommand(Login);
            this.AddCommand = new DelegateCommand(ShowAddForm);
            this.DeleteReservationCommand = new DelegateCommand(DeleteReservation);
        }

        /*Login Process*/
        private async void Login()
        {
            await LoginAsync();
        }

        private async Task LoginAsync()
        {
            try
            {
                userDto = await srv.LoginAsync(userLogin.Email, userLogin.Password);
                if (userDto == null)
                {
                    MessageBox.Show("not connected");
                }
                else
                {
                    MessageBox.Show("connected");
                    Reservations = new ObservableCollection<ReservationDto>(userDto.reservations);

                    // Create an instance of ReservationControl
                    ReservationControl reservationControl = new ReservationControl();

                    // Set the DataContext for ReservationControl using the existing instance of MainWindowBuisness
                    reservationControl.DataContext = this;  // Set it to the current instance

                    // Set ReservationControl as the content of ContentGrid
                    mainWindow.ContentGrid.Children.Clear(); // Clear existing content
                    mainWindow.ContentGrid.Children.Add(reservationControl);

                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        /*Delete process Reservation*/
        private async void DeleteReservation()
        {
            // Check if a reservation is selected in the DataGrid
            if (SelectedReservation != null)
            {
                
                // Call the WCF service to delete the reservation
                var success = await DeleteReservationAsync(SelectedReservation.id);

                if (success)
                {
                    MessageBox.Show($"Deleted Reservation: {SelectedReservation.book.bookTitle}");
                    Reservations.Remove(SelectedReservation);

                }
                else
                {
                    MessageBox.Show("Failed to delete the reservation.");
                }
            }
            else
            {
                MessageBox.Show("No reservation selected.");
            }
        }

        private async Task<bool> DeleteReservationAsync(int reservationId)
        {
            try
            {
                // Call the WCF service method to delete a reservation
                return await srv.DeleteReservationAsync(reservationId);
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
                return false;
            }
        }


        /*Add form Logic*/
        private void ShowAddForm()
        {
            AddForm addForm = new AddForm(userDto.id, Reservations); 
            addForm.ShowDialog();
        }









        /*********************/
    }

}
