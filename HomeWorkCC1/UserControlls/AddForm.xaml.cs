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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UI.Buisness;
using s = ServiceReference1;


namespace UI.UserControlls
{
    /// <summary>
    /// Interaction logic for AddForm.xaml
    /// </summary>
    public partial class AddForm : Window, INotifyPropertyChanged
    {
        s.Service1Client srv = new s.Service1Client();

        private List<BookDto>? _allBooks;
        
        /*all books*/
        public List<BookDto>? AllBooks
        {
            get { return _allBooks; }
            set
            {
                _allBooks = value;
                OnPropertyChanged(nameof(AllBooks));
            }
        }
        /*selected book*/
        private BookDto? _selectedBook;

        public BookDto? SelectedBook
        {
            get { return _selectedBook; }
            set
            {
                _selectedBook = value;
                OnPropertyChanged(nameof(SelectedBook));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /*ctor*/
        public DelegateCommand ReserveCommand { get; set; }
        public int userId { get; }
        public ObservableCollection<ReservationDto> Reservations { get; }

        public  AddForm()
        {
            InitializeComponent();
            this.DataContext = this;
            InitializeAsync();
            this.ReserveCommand = new DelegateCommand(ReserveBook);
        }

       

        public AddForm(int userId, ObservableCollection<ReservationDto> reservations): this()
        {
            this.userId = userId;
            Reservations = reservations;
        }




        /*Methods*/
        private async void InitializeAsync()
        {
            await GetAllBooks();
        }

        private async Task GetAllBooks()
        {
            try
            {
                var allBooksArray = await srv.GetAllBooksAsync();
                AllBooks = allBooksArray.ToList();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                MessageBox.Show($"Error getting books: {ex.Message}");
            }
        }
        /*Reservation Process*/
        private async void ReserveBook()
        {
            // Check if a book is selected in the ListBox
            if (SelectedBook != null)
            {
                // Call the WCF service to add a reservation
                ReservationDto? newRes = await AddReservationAsync(userId, SelectedBook.id);

                if (newRes!=null)
                {
                    MessageBox.Show($"Reserved Book: {SelectedBook.bookTitle}");
                    Reservations.Add(newRes);
                  
                }
                else
                {
                    MessageBox.Show("Failed to reserve the book.");
                }
            }
            else
            {
                MessageBox.Show("No book selected.");
            }
        }


        private async Task<ReservationDto?> AddReservationAsync(int userId, int bookId)
        {
            try
            {
                // Call the WCF service method to add a reservation
                return await srv.AddReservationAsync(userId, bookId);
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
                return null;
            }
        }

    }
}
