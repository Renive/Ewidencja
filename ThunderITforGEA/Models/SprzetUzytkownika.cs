//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ThunderITforGEA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SprzetUzytkownika
    {
        public string Id_ua { get; set; }
        public string rodzajSprzetu { get; set; }
        public string marka { get; set; }
        public string nrPUK { get; set; }
        public string nr_tel { get; set; }
        public string procentZuzycia { get; set; }
        public string model { get; set; }
        public string serviceTag { get; set; }
        public string serialNumber { get; set; }
        public string numerPin { get; set; }
        public string nrTuszu { get; set; }
        public string id_user { get; set; }
        public string pojemnosc { get; set; }
        public string rodzaj { get; set; }
        public string przyjecie { get; set; }
        public string zwrot { get; set; }
        public string moc { get; set; }
        public string numerRejestracyjny { get; set; }
        public string numerVin { get; set; }
        public string opony { get; set; }
        public string ubezpieczenie { get; set; }
        public string przeglad { get; set; }
        public string przebieg { get; set; }
        public string zwracany { get; set; }
        public Nullable<bool> torba { get; set; }
        public Nullable<bool> zasilacz { get; set; }
        public Nullable<bool> mysz { get; set; }
        public Nullable<bool> pamiec_usb { get; set; }
        public Nullable<bool> modem_iplus { get; set; }
        public Nullable<bool> bluetooth { get; set; }
        public Nullable<bool> wifi { get; set; }
        public Nullable<bool> naped_optyczny { get; set; }
        public Nullable<bool> sluchawki { get; set; }
        public Nullable<bool> dysk_przenosny { get; set; }
        public Nullable<bool> drukarka_przenosna { get; set; }
        public Nullable<bool> nagrywarka_przenosna { get; set; }
        public Nullable<bool> samochod_sprawny { get; set; }
        public Nullable<bool> zestaw_glosnomowiacy { get; set; }
        public Nullable<bool> radio { get; set; }
        public Nullable<bool> apteczka { get; set; }
        public Nullable<bool> trojkat { get; set; }
        public Nullable<bool> gasnica { get; set; }
        public Nullable<bool> opony_zimowe { get; set; }
        public Nullable<bool> opony_letnie { get; set; }
        public Nullable<bool> kolo_zapasowe { get; set; }
        public Nullable<bool> ksiazka_serwisowa { get; set; }
        public Nullable<bool> karta_pojazdu { get; set; }
        public Nullable<bool> dowod_rejestracyjny { get; set; }
        public Nullable<bool> karta_paliwowa { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
