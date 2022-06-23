using System;
using System.Collections.Generic;

namespace PrzemysławTrubiłoZad4
{
    class Program
    {
        static void Main(string[] args)
        {
            TestPupilaOpiekuna1();
        }

    }
        /* Pracownik
        *  Ma proste pola Imię, Nazwisko i Stanowisko, oraz strukturę hierarchiczną przełożonych i podwładnych
        *  Własności dostępowe Przełożony i Podwładni pilnują, żeby zawsze relacja była obustronna
        */
    class Pracownik
    {
        public string
            Imię,
            Nazwisko,
            Stanowisko
        ;
        Pracownik
            przełożony = null
        ;
        public HashSet<Pracownik>
            podwładni = new HashSet<Pracownik>()
        ;

        /* pilnujemy by pracownik po zmianie przełożonego był jako podwładny u niego i tylko u niego */
        public Pracownik Przełożony
        {
            get { return przełożony; }
            set
            {
                if (przełożony != null)
                    przełożony.podwładni.Remove(this);
                przełożony = value;
                if (przełożony != null)
                    value.podwładni.Add(this);
            }
        }
        /* zwracamy kopię, aby uniknąć niekontrolowanego dodawania podwładnych*/
        public HashSet<Pracownik> Podwładni
        {
            get { return new HashSet<Pracownik>(podwładni); }
        }

        public Pracownik(
            string imię,
            string nazwisko,
            string stanowisko,
            Pracownik przełożony = null
            )
        {
            Imię = imię;
            Nazwisko = nazwisko;
            Stanowisko = stanowisko;
            Przełożony = przełożony;
        }
        /* nadpisujemy ToString() aby wypisywać od razu stanowisko pracownika */
        public override string ToString()
        {
            return $"{Stanowisko} {Imię} {Nazwisko}";
        }
        /* metoda pozwala na znalezienie pracownika wśród podwładnych, nie tylko bezpośrednich */
        public bool SprawdźWśródPodwładnych(Pracownik szukany)
        {
            bool wynik = false;
            foreach (Pracownik podwładny in Podwładni)
                wynik = (wynik || podwładny == szukany || podwładny.SprawdźWśródPodwładnych(szukany));
            return wynik;
        }
        /* metoda pozwala na wypisanie struktury podwładnych wybranego pracownika */
        public string StrukturaPodwładnych(string prefiks = "")
        {
            string wynik = $"{prefiks}{this}\n";
            foreach (Pracownik podwładny in Podwładni)
                wynik += podwładny.StrukturaPodwładnych("\t" + prefiks);
            return wynik;
        }

    }

}
