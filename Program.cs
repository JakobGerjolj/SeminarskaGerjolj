using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Collections;
using System.Net.Configuration;

namespace SeminarskaJakobGerjolj
{
    //"Izjavljam, da sem nalogo opravil samostojno in da sem njen avtor. Zavedam se, da v primeru, če izjava prvega stavka ni resnična, kršim disciplinska pravila."
    public class Vozel<T>
    {
        private T podatek; // zasebno polje, ki hrani podatke generičnega tipa
        private Vozel<T> naslednji; // referenca na naslednjega
        public Vozel() // konstruktor
        {
            this.Vsebina = default(T);
            this.Nasl = null; // kazalec (referenca) na naslednji element
        }
        //dodatni konstruktor
        public Vozel(T podatek)
            : this()//dedujemo bazični konstruktor
        {
            this.Vsebina = podatek;
        }
        public T Vsebina  //lastnost/Property
        {
            get { return this.podatek; }
            set { this.podatek = value; }
        }
        public Vozel<T> Nasl
        {
            get { return this.naslednji; }
            set { this.naslednji = value; }
        }
    }




    public class Pes
    {
        public string ime;
        public string pasma;
        public int starost;

        public Pes()
        {
            ime = "Pika";
            pasma = "Maltežan";
            starost = 5;

        }
        public Pes(string ime, string pasma, int starost)
        {
            this.ime = ime;
            this.pasma = pasma;
            this.starost = starost;
        }

        public string vrniIme()
        {


            return ime;
        }
        public string vrniPasmo()
        {
            return pasma;
        }
        public int vrniStarost()
        {

            return starost;
        }

        public void vpisiIme(string ims)
        {
            if (Regex.IsMatch(ims, @"^[a-zA-Zčšž]+$"))
            {

                ime = ims;
            }
            else
            {
                Console.WriteLine("Vnesli ste neprimerno ime, vnasam osnovno verzijo!!!!!");
                ime = "Pika";


            }

        }
        public void vpisiPasmo(string pasma1)
        {
            if (Regex.IsMatch(pasma1, @"^[a-zA-Zčšž]+$"))
            {

                pasma = pasma1;
            }
            else
            {
                Console.WriteLine("Vnesli ste neprimerno ime, vnasam osnovno verzijo!!!!!");
                pasma = "Maltezan";


            }

        }
        public void vpisiStarost(int staroas)
        {
            if (Regex.IsMatch(Convert.ToString(staroas), @"^[0-9]+$"))
            {

                starost = staroas;
            }
            else
            {
                Console.WriteLine("Vnesli ste neprimerno starost, vnasam osnovno verzijo!!!!!");
                starost = 5;


            }


        }
        public override string ToString()
        {
            return "Pesu je ime " + ime + " Star je " + starost + " in je pasme " + pasma;

        }


    }










    public class GeneričnaZbirka<T>
    {
        private T[] elementi;  //tabelarično polje
        private int velikost;   //polje hrani trenutno število podatkov v tabeli    
        public GeneričnaZbirka(int n = 0)  //konstruktor
        { elementi = new T[n]; velikost = n; }//začetna dimenzija tabele/zbirke  
        public T this[int indeks]   //indeksiranje 
        {
            get { return elementi[indeks]; } //dostop do posameznih polj
            set { elementi[indeks] = value; }  //prirejanje vrednostim poljem
        }
        //napišimo property, s katerim pridobimo atribut velikost
        public int Velikost
        {
            get { return velikost; }
        }

        //še get metoda za prodobivanje polja velikost
        public int VrneVelikost()
        {
            return velikost;
        }

        public void OdstraniVse()
        {
            elementi = new T[0];
            velikost = 0;
        }
        public void Add(T podatek)  //metoda za dodajanje novega elementa 
        {
            Array.Resize(ref elementi, elementi.Length + 1);
            elementi[velikost] = podatek;  //podatek zapišemo v prvo prosto celico
            velikost = velikost + 1; //število zasedenih celic
        }
        //generična metoda za brisanje celice z določenim indeksom
        public void Brisanje(int indeksCelice)
        {
            if (velikost == 0)
                Console.WriteLine("Zbirka je prazna, brisanje NI možno!");
            //celico brišemo le, če je njen indeks manjši od dimenzije zbirke  
            // if (indeksCelice < elementi.Length && indeksCelice >= 0)
            else if (indeksCelice < elementi.Length)
            {
                T[] zacasna = new T[elementi.Length - 1];
                int j = 0;
                for (int i = 0; i < elementi.Length; i++)
                {
                    if (i != indeksCelice)
                    {
                        zacasna[j] = elementi[i];
                        j++;
                    }
                }
                elementi = zacasna;
                velikost = velikost - 1;//zmanjšamo velikost zbirke
            }
            else Console.WriteLine("Brisanje NI možno, ker indeks št " + indeksCelice + " NE obstaja!");
        }
        //generična metoda za izpis poljubne zbirke
        public void IzpisZbirke()
        {
            if (velikost == 0)
                Console.WriteLine("Zbirka je prazna!");
            else
            {
                Console.WriteLine("Izpis ZBIRKE: ");
                for (int i = 0; i < elementi.Length; i++)
                    Console.WriteLine(elementi[i].ToString() + " ");
                Console.WriteLine();
            }
        }
    }
















    internal class Program
    {


        static bool JeVSeznamu<T>(Vozel<T> prvi, T podatek)
        {
            bool najden = false;
            while (prvi != null)
            {
                if (prvi.Vsebina.Equals(podatek))
                {
                    najden = true;
                    break;
                }
                prvi = prvi.Nasl;
            }
            return najden;
        }


        static void VstaviNaZacetek<T>(ref Vozel<T> prvi, T podatek)
        {
            Vozel<T> zacasni = new Vozel<T>(podatek);
            zacasni.Nasl = prvi;
            prvi = zacasni;
        }


        static int SteviloVozlov<T>(Vozel<T> prvi)
        {
            int st = 0;
            while (prvi != null)
            {
                st++;
                prvi = prvi.Nasl;
            }
            return st;
        }



        static void Serializacija<T>(List<T> list, string fileName)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<T>));
            using (var stream = System.IO.File.OpenWrite(fileName))
            {
                serializer.Serialize(stream, list);
            }
        }
        static void Deserializacija<T>(List<T> list, string fileName)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<T>));
            using (var stream = System.IO.File.OpenRead(fileName))
            {
                var other = (List<T>)(serializer.Deserialize(stream));
                list.Clear();
                list.AddRange(other);
            }
        }


        //Zacetek metode za zbirko, poizvedba: Select * FROM PES where IME like X%;
        static void PoizvedbaZbirka(GeneričnaZbirka<Pes> zbir1)
        {
            string temp1;
            string mona4;
            Console.WriteLine("Izpisal bom vse pse, ki se zacnejo na X");
            while (true)
            {
                Console.Write("Vnesi X: ");
                mona4= Console.ReadLine();
                if (!Regex.IsMatch(mona4, @"^[a-zA-Zčšž]+$") || mona4.Length!=1)
                {
                    Console.WriteLine("Vnesel si nevljavno vrednost! Probaj še enkrat prosim:");
                    // mona1 = Console.ReadLine();

                }
                else break;
            }
            int siy = zbir1.Velikost;

            for(int x=0;x<siy;x++)
            {
                temp1 = zbir1[x].vrniIme();
                if (temp1.Substring(0,1).Equals(mona4))
                {
                    Console.WriteLine(zbir1[x].ToString());

                }

            }





            Console.WriteLine("Izpisal sem vse izpise, ki se ujemoajo z pogojem");
        }


        //Vrni stack metoda
        static Stack<T> Vrnistack<T>(GeneričnaZbirka<T> igg)
        {
            int siys;
            siys = igg.VrneVelikost();
            Stack<T> bobi=new Stack<T>(siys);
        

            for(int x = 0; x < siys; siys++)
            {
                bobi.Push(igg[x]);



            }


            return bobi;

        }

      
        static void Main(string[] args)
        {

            string izbirastrdo;

            string izbira;
            bool B = false, G = false, V = false;
            List<Pes> list1 = new List<Pes>();
            List<Pes> list2 = new List<Pes>();
            bool vpisano = false;
            Vozel<Pes> zacetek = new Vozel<Pes>();
            //Console.ReadKey();
            while (true)
            {
               
                if (B == false && G == false && V == false) //Opcije niso zmeraj prikazane
                {

                    Console.WriteLine("1.BAZA");
                    Console.WriteLine("2.GENERIČNA ZBIRKA");
                    Console.WriteLine("3.VERIŽNI SEZNAM");
                    Console.WriteLine("4.UGASNI");
                    Console.Write("Vnesi izbiro: ");
                    
                   
                }
                izbira = Console.ReadLine();


                if (izbira.Equals("4"))
                {

                    break;
                }


                if (izbira.Equals("5"))
                {
                   
                    string connectionString = @"Data Source= (LocalDB)\MSSQLLocalDB   ;AttachDbFilename= |DataDirectory|Database.mdf;Integrated Security=True;Connect Timeout=30";
                    SqlConnection dataConnection = new SqlConnection(connectionString);
                    string poizvedba = "SELECT * FROM Pes;";  //poizvedba
                                                              //ali pa npr string poizvedba = "SELECT * FROM Drzave where Kontinent= ‘ Evropa‘;";  //poizvedba
                    SqlCommand dataCommand = new SqlCommand(poizvedba, dataConnection);  //SQL ukaz
                    dataConnection.Open();  //odpiranje povezave
                    SqlDataReader reader = dataCommand.ExecuteReader(); //objekt za branje podatkov  iz poizvedbe
                    while (reader.Read()) //podatke beremo dokler obstajajo (dokler ne pridemo do konca tabele)
                    {
                        //metoda GetValue vrne vrednost polja z indeksom prebranega zapisa 
                        Console.WriteLine(reader.GetValue(0) + " --" + reader.GetValue(1) + " --" + reader.GetValue(2) + " --" + reader.GetValue(3));
                    }
                    reader.Close();  //zapiranje objekta za branje
                    dataConnection.Close();
                    Console.ReadKey();


                    //Console.WriteLine("TestNigger");
                    //string connectionString = @"Data Source= (LocalDB)\MSSQLLocalDB   ;AttachDbFilename= |DataDirectory|Database.mdf;Integrated Security=True;Connect Timeout=30";
                    //SqlConnection dataConnection = new SqlConnection(connectionString);
                    //string poizvedba = "DELETE FROM Pes;";  //poizvedba
                    //SqlCommand dataCommand = new SqlCommand(poizvedba, dataConnection);
                    //dataConnection.Open();
                    //dataCommand.ExecuteNonQuery();

                    //dataConnection.Close();






                    break;
                }
                if (izbira.Equals("1") || B == true)
                {
                    B = true;
                    string izbiraBAZA;
                    Console.WriteLine("Izbral si bazo");
                    Console.WriteLine("1 : Prikazi tabelo");
                    Console.WriteLine("2 : Dodaj zapis");
                    Console.WriteLine("3 : Spremeni zapis");
                    Console.WriteLine("4 : Izbrisi zapis");
                    Console.WriteLine("5 : Pokazi tiste, ki so starejsi od X starosti");
                    Console.WriteLine("6 : Pokazi pse, ki so pasme X");
                    Console.WriteLine("7 : Pokazi pse, katerih ime se zacne na crko X");
                    Console.WriteLine("8 : Izpisi povprecno starost vseh psov");
                    Console.WriteLine("9 : Izpisi sesteto starost vseh psov");
                    Console.WriteLine("10: Izpisi stevilo psov, ki imajo ime X");
                    Console.WriteLine("11: Zakljuci in vpisi podatke v drugo XML datoteko");
                    Console.Write("Napisi izbiro: ");
                    izbiraBAZA = Console.ReadLine();
                    Deserializacija<Pes>(list1, "Datoteka2.XML");
                    string connectionString = @"Data Source= (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Database.mdf;Integrated Security=True;Connect Timeout=30";
                    SqlConnection dataConnection = new SqlConnection(connectionString);
                    dataConnection.Open(); //odpiranje povezave s strežnikom
                    string sprem1 = list1[0].vrniIme();
                    string sprem2 = list1[0].vrniPasmo();
                    int sprem3 = list1[0].vrniStarost();
                    string sprem11 = list1[1].vrniIme();
                    string sprem22 = list1[1].vrniPasmo();
                    int sprem33 = list1[1].vrniStarost();

                    if (vpisano == false)
                    {
                        foreach (var das in list1)
                        {
                            string poizvedba = "INSERT INTO Pes (IME,PASMA,STAROST) VALUES(@parameter1,@parameter2,@parameter3)";
                            using (SqlCommand dataCommand = new SqlCommand(poizvedba, dataConnection))
                            {
                                dataCommand.Parameters.AddWithValue("@parameter1", das.vrniIme());
                                dataCommand.Parameters.AddWithValue("@parameter2", das.vrniPasmo());
                                dataCommand.Parameters.AddWithValue("@parameter3", das.vrniStarost());
                                dataCommand.ExecuteNonQuery();



                            }

                        }
                        vpisano = true;
                    }





                     dataConnection.Close();








                    if (izbiraBAZA.Equals("1"))
                    {
                        
                        string poizvedba = "SELECT * FROM Pes;";  //poizvedba
                                                                  //ali pa npr string poizvedba = "SELECT * FROM Drzave where Kontinent= ‘ Evropa‘;";  //poizvedba
                        SqlCommand dataCommand = new SqlCommand(poizvedba, dataConnection);  //SQL ukaz
                        dataConnection.Open();  //odpiranje povezave
                        SqlDataReader reader = dataCommand.ExecuteReader(); //objekt za branje podatkov  iz poizvedbe
                        while (reader.Read()) //podatke beremo dokler obstajajo (dokler ne pridemo do konca tabele)
                        {
                            //metoda GetValue vrne vrednost polja z indeksom prebranega zapisa 
                            Console.WriteLine(reader.GetValue(0) + " --" + reader.GetValue(1) + " --" + reader.GetValue(2) + " --" + reader.GetValue(3));
                        }
                        reader.Close();  //zapiranje objekta za branje
                        dataConnection.Close();
                      //  Console.ReadKey();


                    }

                    if (izbiraBAZA.Equals("2"))
                    {
                        dataConnection.Open();
                        string cunt1;
                        string cunt2;
                        int cunt3;


                        Console.WriteLine();
                        Console.Write("Vnesi ime: ");
                        cunt1 = Console.ReadLine();
                        if (!Regex.IsMatch(cunt1, @"^[a-zA-Zčšž]+$"))
                        {
                            Console.WriteLine("Vnesel si nevljavno vrednost!!!! ( navadna vrednost bo vnešena: Pika)");
                            cunt1 = "Pika";

                        }



                        Console.WriteLine("Vnesi pasmo: ");
                        cunt2 = Console.ReadLine();
                        if (!Regex.IsMatch(cunt2, @"^[a-zA-Zčšž]+$"))
                        {
                            Console.WriteLine("Vnesel si nevljavno vrednost!!!! ( navadna vrednost bo vnešena: Maltezan)");
                            cunt2 = "Maltezan";

                        }
                        Console.WriteLine("Vnesi starost: ");
                        cunt3 = Convert.ToInt32(Console.ReadLine());
                        if (!Regex.IsMatch(Convert.ToString(cunt3), @"^[0-9]+$"))
                        {
                            Console.WriteLine("Vnesel si nevljavno vrednost!!!! ( navadna vrednost bo vnešena: 5)");
                            cunt3 = 5;

                        }

                        string poizvedba = "INSERT INTO Pes (IME,PASMA,STAROST) VALUES(@parameter1,@parameter2,@parameter3)";
                        using (SqlCommand dataCommand = new SqlCommand(poizvedba, dataConnection))
                        {
                            dataCommand.Parameters.AddWithValue("@parameter1", cunt1);
                            dataCommand.Parameters.AddWithValue("@parameter2", cunt2);
                            dataCommand.Parameters.AddWithValue("@parameter3", cunt3);
                            dataCommand.ExecuteNonQuery();



                        }
                        dataConnection.Close();
                      //  Console.ReadKey();
                    }

                    if (izbiraBAZA.Equals("3"))
                    {
                        dataConnection.Open();
                        
                        int IDzapisa;
                        string vpis1;
                        string vpis2;
                        int vpis3;
                        while (true)
                        {
                            Console.WriteLine("Kateri zapis bi spreminjali (Vpisi ID)?");
                            IDzapisa = Convert.ToInt32(Console.ReadLine());
                            if (!Regex.IsMatch(Convert.ToString(IDzapisa), @"^[0-9]+$"))
                            {
                                Console.WriteLine("Vnesel si nevljavno vrednost! Probaj še enkrat prosim");


                            }
                            else break;
                        }

                        Console.Write("Vnesi ime: ");
                        vpis1 = Console.ReadLine();
                        if (!Regex.IsMatch(vpis1, @"^[a-zA-Zčšž]+$"))
                        {
                            Console.WriteLine("Vnesel si nevljavno vrednost!!!! ( navadna vrednost bo vnešena: Pika)");
                            vpis1 = "Pika";

                        }



                        Console.WriteLine("Vnesi pasmo: ");
                        vpis2 = Console.ReadLine();
                        if (!Regex.IsMatch(vpis2, @"^[a-zA-Zčšž]+$"))
                        {
                            Console.WriteLine("Vnesel si nevljavno vrednost!!!! ( navadna vrednost bo vnešena: Maltezan)");
                            vpis2 = "Maltezan";

                        }
                        Console.WriteLine("Vnesi starost: ");
                        vpis3 = Convert.ToInt32(Console.ReadLine());
                        if (!Regex.IsMatch(Convert.ToString(vpis3), @"^[0-9]+$"))
                        {
                            Console.WriteLine("Vnesel si nevljavno vrednost!!!! ( navadna vrednost bo vnešena: 5)");
                            vpis3 = 5;

                        }
                        string poizvedba = "UPDATE Pes SET IME = @parameter1, PASMA = @parameter2, STAROST = @parameter3 WHERE ID = @parameter4";
                        SqlCommand dataCommand = new SqlCommand(poizvedba, dataConnection);
                        dataCommand.Parameters.AddWithValue("@parameter4", IDzapisa);
                        dataCommand.Parameters.AddWithValue("@parameter1", vpis1);
                        dataCommand.Parameters.AddWithValue("@parameter2", vpis2);
                        dataCommand.Parameters.AddWithValue("@parameter3", vpis3);


                        dataCommand.ExecuteNonQuery(); //

                        dataConnection.Close();
                       // Console.ReadKey();
                    }
                    if (izbiraBAZA.Equals("4"))
                    {
                        dataConnection.Open();

                        int IDzapisa1;
                      
                        while (true)
                        {
                            Console.WriteLine("Kateri zapis bi izbrisali (Vpisi ID)?");
                            IDzapisa1 = Convert.ToInt32(Console.ReadLine());
                            if (!Regex.IsMatch(Convert.ToString(IDzapisa1), @"^[0-9]+$"))
                            {
                                Console.WriteLine("Vnesel si nevljavno vrednost! Probaj še enkrat prosim:");
                                IDzapisa1 = Convert.ToInt32(Console.ReadLine());

                            }
                            else break;
                        }

                      
                        string poizvedba = "DELETE FROM Pes WHERE ID = @parameter4";
                        SqlCommand dataCommand = new SqlCommand(poizvedba, dataConnection);
                        dataCommand.Parameters.AddWithValue("@parameter4", IDzapisa1);
                   


                        dataCommand.ExecuteNonQuery(); //izvedba 








                        dataConnection.Close();
                     //   Console.ReadKey();
                    }


                    if (izbiraBAZA.Equals("5"))
                    {

                        int mona;

                        Console.WriteLine("Izpisi vse, ki so starejsi od X starosti!");
                        
                        while (true)
                        {
                            Console.Write("Vnesi X: ");
                            mona = Convert.ToInt32(Console.ReadLine());
                            if (!Regex.IsMatch(Convert.ToString(mona), @"^[0-9]+$"))
                            {
                                Console.WriteLine("Vnesel si nevljavno vrednost! Probaj še enkrat prosim:");
                                mona = Convert.ToInt32(Console.ReadLine());

                            }
                            else break;
                        }

                        

                        string poizvedba = "SELECT * FROM Pes WHERE STAROST>"+ mona+";";  //poizvedba
                                                                  //ali pa npr string poizvedba = "SELECT * FROM Drzave where Kontinent= ‘ Evropa‘;";  //poizvedba
                        SqlCommand dataCommand = new SqlCommand(poizvedba, dataConnection);  //SQL ukaz
                        dataConnection.Open();  //odpiranje povezave
                        SqlDataReader reader = dataCommand.ExecuteReader(); //objekt za branje podatkov  iz poizvedbe
                        while (reader.Read()) //podatke beremo dokler obstajajo (dokler ne pridemo do konca tabele)
                        {
                            //metoda GetValue vrne vrednost polja z indeksom prebranega zapisa 
                            Console.WriteLine(reader.GetValue(0) + " --" + reader.GetValue(1) + " --" + reader.GetValue(2) + " --" + reader.GetValue(3));
                        }
                        reader.Close();  //zapiranje objekta za branje
                        dataConnection.Close();
                      //  Console.ReadKey();


                    }

                    if (izbiraBAZA.Equals("6"))
                    {

                        string mona1;

                        Console.WriteLine("Izpisi vse, ki so pasme X!");

                        while (true)
                        {
                            Console.Write("Vnesi X: ");
                            mona1 = Console.ReadLine();
                            if (!Regex.IsMatch(Convert.ToString(mona1), @"^[a-zA-Zčšž]+$"))
                            {
                                Console.WriteLine("Vnesel si nevljavno vrednost! Probaj še enkrat prosim:");
                               // mona1 = Console.ReadLine();

                            }
                            else break;
                        }



                        string poizvedba = "SELECT * FROM Pes WHERE PASMA ='" + mona1 + "';";  //poizvedba
                                                                                             //ali pa npr string poizvedba = "SELECT * FROM Drzave where Kontinent= ‘ Evropa‘;";  //poizvedba
                        SqlCommand dataCommand = new SqlCommand(poizvedba, dataConnection);  //SQL ukaz
                        dataConnection.Open();  //odpiranje povezave
                        SqlDataReader reader = dataCommand.ExecuteReader(); //objekt za branje podatkov  iz poizvedbe
                        while (reader.Read()) //podatke beremo dokler obstajajo (dokler ne pridemo do konca tabele)
                        {
                            //metoda GetValue vrne vrednost polja z indeksom prebranega zapisa 
                            Console.WriteLine(reader.GetValue(0) + " --" + reader.GetValue(1) + " --" + reader.GetValue(2) + " --" + reader.GetValue(3));
                        }
                        reader.Close();  //zapiranje objekta za branje
                        dataConnection.Close();
                      //  Console.ReadKey();


                    }

                    if (izbiraBAZA.Equals("7"))
                    {

                        string mona2;

                        Console.WriteLine("Izpisi tiste pse, katerih ime se zacne na crko X!");

                        while (true)
                        {
                            Console.Write("Vnesi X: ");
                            mona2 = Console.ReadLine();
                            if (!Regex.IsMatch(mona2, @"^[a-zA-Zčšž]+$") || mona2.Length!=1)
                            {
                                Console.WriteLine("Vnesel si nevljavno vrednost! Probaj še enkrat prosim:");
                                // mona1 = Console.ReadLine();

                            }
                            else break;
                        }



                        string poizvedba = "SELECT * FROM Pes WHERE IME LIKE '" + mona2 + "%';";  //poizvedba
                                                                                               //ali pa npr string poizvedba = "SELECT * FROM Drzave where Kontinent= ‘ Evropa‘;";  //poizvedba
                        SqlCommand dataCommand = new SqlCommand(poizvedba, dataConnection);  //SQL ukaz
                        dataConnection.Open();  //odpiranje povezave
                        SqlDataReader reader = dataCommand.ExecuteReader(); //objekt za branje podatkov  iz poizvedbe
                        while (reader.Read()) //podatke beremo dokler obstajajo (dokler ne pridemo do konca tabele)
                        {
                            //metoda GetValue vrne vrednost polja z indeksom prebranega zapisa 
                            Console.WriteLine(reader.GetValue(0) + " --" + reader.GetValue(1) + " --" + reader.GetValue(2) + " --" + reader.GetValue(3));
                        }
                        reader.Close();  //zapiranje objekta za branje
                        dataConnection.Close();
                      //  Console.ReadKey();


                    }

                    if (izbiraBAZA.Equals("8"))
                    {

                        

                        Console.WriteLine("Izpisujem povprecno leto vseh psov!");





                        using (SqlConnection dataConn = new SqlConnection(connectionString))
                        {
                            dataConn.Open();
                            //poizvedba    
                            string poizvedba = "SELECT AVG(STAROST) FROM PES";
                            using (SqlCommand dataCommand = new SqlCommand(poizvedba, dataConn))
                            {
                                Console.WriteLine("Povprecna starost psov: " + dataCommand.ExecuteScalar().ToString());
                            }
                        }

                        dataConnection.Close();
                     //   Console.ReadKey();


                    }


                    if (izbiraBAZA.Equals("9"))
                    {



                        Console.WriteLine("Izpisujem sestevek let vseh psov!");





                        using (SqlConnection dataConn = new SqlConnection(connectionString))
                        {
                            dataConn.Open();
                            //poizvedba    
                            string poizvedba = "SELECT SUM(STAROST) FROM PES";
                            using (SqlCommand dataCommand = new SqlCommand(poizvedba, dataConn))
                            {
                                Console.WriteLine("Skupna starost psov: " + dataCommand.ExecuteScalar().ToString());
                            }
                        }

                        dataConnection.Close();
                     //   Console.ReadKey();


                    }

                    if (izbiraBAZA.Equals("10"))
                    {



                        


                        string mona3;

                        Console.WriteLine("Izpisi stevilo psov, ki imajo ime X!");

                        while (true)
                        {
                            Console.Write("Vnesi X: ");
                            mona3 = Console.ReadLine();
                            if (!Regex.IsMatch(mona3, @"^[a-zA-Zčšž]+$"))
                            {
                                Console.WriteLine("Vnesel si nevljavno vrednost! Probaj še enkrat prosim:");
                                // mona1 = Console.ReadLine();

                            }
                            else break;
                        }



                        using (SqlConnection dataConn = new SqlConnection(connectionString))
                        {
                            dataConn.Open();
                            //poizvedba    
                            string poizvedba = "SELECT COUNT(ID) FROM PES WHERE IME ='" + mona3+ "'";
                            using (SqlCommand dataCommand = new SqlCommand(poizvedba, dataConn))
                            {
                                Console.WriteLine("Stevilo psov, ki imajo ime "+mona3+" psov: " + dataCommand.ExecuteScalar().ToString());
                            }
                        }

                        dataConnection.Close();
                      //  Console.ReadKey();


                    }


                    if (izbiraBAZA.Equals("11"))
                    {
                        string s1, s2, s3;
                        string poizvedba = "SELECT * FROM Pes;";  //poizvedba
                        List<Pes> list13 = new List<Pes>();                                          //ali pa npr string poizvedba = "SELECT * FROM Drzave where Kontinent= ‘ Evropa‘;";  //poizvedba
                        SqlCommand dataCommand = new SqlCommand(poizvedba, dataConnection);  //SQL ukaz
                        dataConnection.Open();  //odpiranje povezave
                        SqlDataReader reader = dataCommand.ExecuteReader(); //objekt za branje podatkov  iz poizvedbe
                        while (reader.Read()) //podatke beremo dokler obstajajo (dokler ne pridemo do konca tabele)
                        {
                            //metoda GetValue vrne vrednost polja z indeksom prebranega zapisa 
                            Console.WriteLine(reader.GetValue(0) + " --" + reader.GetValue(1) + " --" + reader.GetValue(2) + " --" + reader.GetValue(3));
                            s1 = reader.GetValue(1)+"";
                            s2 = reader.GetValue(2) + "";
                            s3 = reader.GetValue(3) + "";
                            list13.Add(new Pes(s1, s2, Convert.ToInt32(s3)));


                        }
                        Serializacija<Pes>(list13, "Dokument3.XML");








                        Console.WriteLine("Serializacija uspela, zapiram program, praznim bazo");
                        Console.ReadKey();




                        reader.Close();  //zapiranje objekta za branje
                        dataConnection.Close();


                        dataConnection.Open();

                        

                        


                        poizvedba = "DELETE FROM Pes";
                        dataCommand = new SqlCommand(poizvedba, dataConnection);
                        




                        dataCommand.ExecuteNonQuery(); //izvedba 








                        dataConnection.Close();









                        
                        //Console.ReadKey();

                        break;
                    }






                    dataConnection.Close();
                    Console.WriteLine("Pritisni ENTER da nadaljujes!!");
                   //
                   //Console.ReadKey();





                    

                    
                   
                }
                if (izbira.Equals("2") || G==true)
                {
                    int izbiraZBIRKA;
                    G = true;
                    Console.WriteLine("Izbral si Generično zbirko");
                    Deserializacija<Pes>(list2, "Datoteka2.XML");
                    GeneričnaZbirka<Pes> Zbirka = new GeneričnaZbirka<Pes>();
                    foreach (var item in list2)
                    {
                        Zbirka.Add(item);//Napolnimo zbirko
                    }
                    Console.WriteLine("1 : Izpis zbirke");
                    Console.WriteLine("2 : Izpis psov, katerih ime se zacne na X");
                    Console.WriteLine("3 : Naredi stack iz zbirke");
                    Console.WriteLine("4 : Prepisi v drugo XML datoteko in zapri");
                    //Console.WriteLine("3 : LINQ-pokazi vse, ki so pasme Maltezan");

                    Console.Write("Vnesi izbiro:");
                    izbiraZBIRKA = Convert.ToInt32(Console.ReadLine());

                    if (izbiraZBIRKA == 1)
                    {

                        Zbirka.IzpisZbirke();
                        Console.WriteLine("Pritisni ENTER da nadaljuješ!");
                      //  Console.ReadKey();
                    }
                    if (izbiraZBIRKA == 2)
                    {

                        PoizvedbaZbirka(Zbirka);
                        Console.WriteLine("Pritisni ENTER da nadaljuješ!");
                        //Console.ReadKey();
                    }

                    if (izbiraZBIRKA == 3)
                    {
                        // Stack <Pes> mos= new Stack<Pes>();
                        //mos = Vrnistack<Pes>(Zbirka);




                        // foreach (var item in mos)
                        //     Console.Write(item.vrniIme() + ",");


                        // Console.ReadKey();
                        Console.WriteLine("Pritisni ENTER da nadaljuješ!");
                    }
                    if(izbiraZBIRKA == 4)
                    {
                        List<Pes> list1345 = new List<Pes>();
                        int tsd = Zbirka.VrneVelikost();

                        for(int x = 0; x < tsd; x++)
                        {
                            Console.WriteLine(Zbirka[x].ToString() + " ZAPISANA V XML");
                            list1345.Add(Zbirka[x]);

                        }
                        Serializacija<Pes>(list1345, "Dokument3.XML");
                        Console.WriteLine("Izvoz uspešen");
                        Console.WriteLine("Pritisni ENTER da nadaljuješ!");
                        Console.ReadKey();
                        break;
                    }


                   
                    //if(izbiraZBIRKA == 3)
                    //{
                    //    var FA1s = (from Pes in Zbirka select Pes);
                    //    int count = 0;
                    //    foreach(Pes a in Zbirka)
                    //    {
                    //        count++;

                    //    }

                    //    Console.WriteLine("Vseh maltezanov je: ");

                    //    Console.ReadKey();

                    //}

                   //
                   //Console.ReadKey();
                }

                
                if (izbira.Equals("3") || V == true)
                {


                    
                    V = true;
                    string izbriaVeriga;
                    Console.WriteLine("Izbral si verižni seznam!");
                    if (vpisano == false)
                    {
                        List<Pes> tempo = new List<Pes>();
                        Deserializacija<Pes>(tempo, "Datoteka2.XML");
                        Vozel<Pes> v1 = new Vozel<Pes>(tempo[0]);
                        Vozel<Pes> start = v1;
                        Vozel<Pes> v2 = new Vozel<Pes>(tempo[1]);
                        Vozel<Pes> v3 = new Vozel<Pes>(tempo[2]);
                        Vozel<Pes> v4 = new Vozel<Pes>(tempo[3]);
                        Vozel<Pes> v5 = new Vozel<Pes>(tempo[4]);
                        Vozel<Pes> v6 = new Vozel<Pes>(tempo[5]);
                        Vozel<Pes> v7 = new Vozel<Pes>(tempo[6]);
                        Vozel<Pes> v8 = new Vozel<Pes>(tempo[7]);
                        Vozel<Pes> v9 = new Vozel<Pes>(tempo[8]);
                        Vozel<Pes> v10 = new Vozel<Pes>(tempo[9]);
                        Vozel<Pes> v11 = new Vozel<Pes>(tempo[10]);
                        Vozel<Pes> v12 = new Vozel<Pes>(tempo[11]);
                        Vozel<Pes> v13 = new Vozel<Pes>(tempo[12]);
                        Vozel<Pes> v14 = new Vozel<Pes>(tempo[13]);
                        Vozel<Pes> v15 = new Vozel<Pes>(tempo[14]);
                        Vozel<Pes> v16 = new Vozel<Pes>(tempo[15]);
                        Vozel<Pes> v17 = new Vozel<Pes>(tempo[16]);
                        Vozel<Pes> v18 = new Vozel<Pes>(tempo[17]);
                        Vozel<Pes> v19 = new Vozel<Pes>(tempo[18]);
                        Vozel<Pes> v20 = new Vozel<Pes>(tempo[19]);
                        v1.Nasl = v2;
                        v2.Nasl = v3;
                        v3.Nasl = v4;
                        v4.Nasl = v5;
                        v5.Nasl = v6;
                        v6.Nasl = v7;
                        v7.Nasl = v8;
                        v8.Nasl = v9;
                        v9.Nasl = v10;
                        v10.Nasl = v11;
                        v11.Nasl = v12;
                        v12.Nasl = v13;
                        v13.Nasl = v14;
                        v14.Nasl = v15;
                        v15.Nasl = v16;
                        v16.Nasl = v17;
                        v17.Nasl = v18;
                        v18.Nasl = v19;
                        v19.Nasl = null;
                        zacetek = v1;
                        vpisano = true;
                        
                    }
                    Console.WriteLine("1 : Izpis seznama");
                    Console.WriteLine("2 : Izpisi stevilo vozlov");
                    Console.WriteLine("3 : Vstavi na zacetek"); 
                    Console.WriteLine("4 : Je v seznamu funkcija");
                    Console.WriteLine("5 : Izpisi v XML");
                    Console.Write("Vnesi izbiro: ");
                    izbriaVeriga = Console.ReadLine();//

                    if (izbriaVeriga.Equals("1"))
                    {

                        Vozel<Pes> zacetekSeznama = zacetek;
                        while (zacetekSeznama != null)
                        {
                            Console.WriteLine("__"+zacetekSeznama.Vsebina.vrniIme() + "__"+ zacetekSeznama.Vsebina.vrniPasmo()+"__"+ zacetekSeznama.Vsebina.vrniStarost());
                            zacetekSeznama = zacetekSeznama.Nasl;
                        }
                        Console.WriteLine("Pritisni ENTER da nadaljuješ!");
                    }

                    if (izbriaVeriga.Equals("2"))
                    {

                        Vozel<Pes> zacetekSeznama = zacetek;
                       Console.WriteLine("Stevilo vozlov v seznamu: "+ SteviloVozlov<Pes>(zacetekSeznama));
                        Console.WriteLine("Pritisni ENTER da nadaljuješ!");
                    }

                    if (izbriaVeriga.Equals("3"))
                    {
                        Vozel<Pes> zacetekSeznama = zacetek;
                        string s1, s2;
                        int mosa;
                        Console.WriteLine("Vstavljanje podatka na zacetek");
                        Console.Write("Vnesi ime:");


                        s1 = Console.ReadLine();
                        if (!Regex.IsMatch(s1, @"^[a-zA-Zčšž]+$"))
                        {
                            Console.WriteLine("Vnesel si nevljavno vrednost!!!! ( navadna vrednost bo vnešena: Pika)");
                            s1 = "Pika";

                        }

                        //
                        Console.WriteLine("Vnesi pasmo:");
                       // Console.WriteLine("Vnesi pasmo: ");
                        s2 = Console.ReadLine();
                        if (!Regex.IsMatch(s2, @"^[a-zA-Zčšž]+$"))
                        {
                            Console.WriteLine("Vnesel si nevljavno vrednost!!!! ( navadna vrednost bo vnešena: Maltezan)");
                            s2 = "Maltezan";

                        }
                        
                        Console.Write("Vnesi starost:");

                        Console.WriteLine("Vnesi starost: ");
                        mosa = Convert.ToInt32(Console.ReadLine());
                        if (!Regex.IsMatch(Convert.ToString(mosa), @"^[0-9]+$"))
                        {
                            Console.WriteLine("Vnesel si nevljavno vrednost!!!! ( navadna vrednost bo vnešena: 5)");
                            mosa = 5;

                        }

                        
                        Pes tms = new Pes(s1,s2,mosa);
                        VstaviNaZacetek<Pes>(ref zacetek, tms);


                        Console.WriteLine("Pritisni ENTER da nadaljuješ!");
                        // Console.ReadKey();
                    }

                    if (izbriaVeriga.Equals("4"))
                    {
                        Vozel<Pes> zacetekSeznama = zacetek;
                        string s1, s2;
                        string mosa;
                        Console.WriteLine("Ali je podatek v seznamu");
                        Pes tms = new Pes("Pika","Maltezan",5);
                        Pes tms1 = new Pes("Pika", "Maltezan", 5);
                        if (!tms.Equals(tms1)) Console.WriteLine("Equals ne deluje");
                        if (JeVSeznamu<Pes>(zacetekSeznama, tms))
                        {
                            Console.WriteLine("JE v seznamu!");


                        }
                        else Console.WriteLine("Ni v seznamu!");



                       // Console.ReadKey();


                        List<Pes> tems = new List<Pes>();
                        Console.WriteLine("Pritisni ENTER da nadaljuješ!");
                    }



                    if (izbriaVeriga.Equals("5"))
                    {
                        List<Pes> tems = new List<Pes>();
                        Vozel<Pes> zacetekSeznama = zacetek;
                        while (zacetekSeznama != null)
                        {
                            tems.Add(zacetekSeznama.Vsebina);
                            zacetekSeznama = zacetekSeznama.Nasl;
                        }
                        Console.WriteLine("Vneseno v list");
                        Serializacija<Pes>(tems, "Dokument3.XML");
                        Console.WriteLine("Serializirano!");
                        Console.WriteLine("Pritisni ENTER da zapreš program!");
                        // Console.ReadKey();
                        break;
                    }


                }







            }



        }
    }
}
