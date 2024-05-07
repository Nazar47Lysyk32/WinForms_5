using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Xml;
using WindowsFormsApp7;

public class ReservationRepository
{
    private List<Reservation> reservations;
    private const string FileName = "reservations.json";

    public ReservationRepository()
    {
        reservations = LoadReservations();
    }

    public bool AddReservation(Reservation reservation)
    {
        if (reservations.Any(r => r.ReservationTime == reservation.ReservationTime))
            return false;
        reservations.Add(reservation);
        SaveReservations();
        return true;
    }

    public List<Reservation> GetReservations()
    {
        return reservations;
    }

    private List<Reservation> LoadReservations()
    {
        if (!File.Exists(FileName))
            return new List<Reservation>();
        string json = File.ReadAllText(FileName);
        return JsonConvert.DeserializeObject<List<Reservation>>(json);
    }

    private void SaveReservations()
    {
        string json = JsonConvert.SerializeObject(reservations, Formatting.Indented);
        File.WriteAllText(FileName, json);
    }
}

