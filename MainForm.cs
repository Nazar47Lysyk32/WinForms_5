using System;
using System.Windows.Forms;
using WindowsFormsApp7;

public partial class MainForm : Form1
{
    private TextBox txtCustomerName;
    private DateTimePicker dateTimePicker;
    private Button btnReserve;
    private ListBox lstReservations;
    private ReservationRepository repository = new ReservationRepository();

    public MainForm()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.txtCustomerName = new TextBox();
        this.dateTimePicker = new DateTimePicker();
        this.btnReserve = new Button();
        this.lstReservations = new ListBox();

        // Setting properties
        this.txtCustomerName.Location = new System.Drawing.Point(12, 12);
        this.txtCustomerName.Size = new System.Drawing.Size(200, 20);

        this.dateTimePicker.Location = new System.Drawing.Point(12, 40);
        this.dateTimePicker.Size = new System.Drawing.Size(200, 20);
        this.dateTimePicker.Format = DateTimePickerFormat.Custom;
        this.dateTimePicker.CustomFormat = "MM/dd/yyyy HH:mm";

        this.btnReserve.Location = new System.Drawing.Point(12, 68);
        this.btnReserve.Size = new System.Drawing.Size(100, 23);
        this.btnReserve.Text = "Reserve";
        this.btnReserve.Click += new EventHandler(btnReserve_Click);

        this.lstReservations.Location = new System.Drawing.Point(12, 100);
        this.lstReservations.Size = new System.Drawing.Size(200, 200);

        // Adding controls to the form
        this.Controls.Add(this.txtCustomerName);
        this.Controls.Add(this.dateTimePicker);
        this.Controls.Add(this.btnReserve);
        this.Controls.Add(this.lstReservations);

        // Form settings
        this.ClientSize = new System.Drawing.Size(224, 321);
        this.Text = "Restaurant Reservation System";
    }

    private void btnReserve_Click(object sender, EventArgs e)
    {
        var reservation = new Reservation
        {
            CustomerName = txtCustomerName.Text,
            ReservationTime = dateTimePicker.Value
        };

        if (repository.AddReservation(reservation))
        {
            MessageBox.Show("Reservation successful!");
            UpdateReservationList();
        }
        else
        {
            MessageBox.Show("This time slot is already booked.");
        }
    }

    private void UpdateReservationList()
    {
        lstReservations.Items.Clear();
        foreach (var reservation in repository.GetReservations())
        {
            lstReservations.Items.Add($"{reservation.CustomerName} - {reservation.ReservationTime:MM/dd/yyyy HH:mm}");
        }
    }
}
