using System.IO;
using System.Text;
using System.Windows;

namespace MuveltsegiVerseny
{
    public partial class MainWindow : Window
    {
        List<Kerdes> kerdesek = new List<Kerdes>();

        public MainWindow()
        {
            InitializeComponent();

            using StreamReader reader = new StreamReader(@"../../../src/feladatok.txt", Encoding.UTF8);
            while (!reader.EndOfStream) kerdesek.Add(new Kerdes(reader.ReadLine()));
            lbxKerdesek.ItemsSource = kerdesek;
        }

        private void Kerdojel_Button_Click(object sender, RoutedEventArgs e)
        {
            txbKerdojel.Text = $"A kérdőjeles feladatok száma: {kerdesek.Count(x => x.MuveltsegiKerdes.Contains('?')).ToString()}";
        }

        private void HaromPont_Click(object sender, RoutedEventArgs e)
        {
            txbHaromPont.Text = $"3 pontos kémia feladatok száma: {kerdesek.Count(x => x.Pont == 3 && x.Temakor == "kemia").ToString()}";
        }

        private void Szamertek_Click(object sender, RoutedEventArgs e)
        {
            int minSzam = kerdesek.Min(x => x.Valasz);
            int maxSzam = kerdesek.Max(x => x.Valasz);
            txbSzamertek.Text = $"A válaszok számértéke {minSzam} és {maxSzam} között terjed.";
        }

        private void TemakorLIsta_Click(object sender, RoutedEventArgs e)
        {
            lbxTemakorok.ItemsSource = kerdesek.Select(x => x.Temakor).Distinct().OrderBy(x => x);
        }

        private void Kereses_Click(object sender, RoutedEventArgs e)
        {
            List<Kerdes> keresettKerdesek = kerdesek.Where(x => x.MuveltsegiKerdes.Contains(txbKifejezes.Text)).ToList();

            if (keresettKerdesek.Count > 0)
            {
                lbxKeresett.ItemsSource = keresettKerdesek;
                if (keresettKerdesek.Count == 1)
                {
                    txbSorsoltKerdes.Text = keresettKerdesek[0].MuveltsegiKerdes.ToString();
                }
                else
                {
                    Random rnd = new Random();
                    int random = rnd.Next(0, keresettKerdesek.Count);
                    txbSorsoltKerdes.Text = keresettKerdesek[random].MuveltsegiKerdes.ToString();
                }
            }
            else
            {
                MessageBox.Show("Nincs találat!");
            }
        }

        private void ValaszKuldes_Click(object sender, RoutedEventArgs e)
        {
            List<Kerdes> keresettKerdesek = kerdesek.Where(x => x.MuveltsegiKerdes.Contains(txbKifejezes.Text)).ToList();

            if (keresettKerdesek.Count == 1)
            {
                int elertPont = ValaszPontozas(txbValasz.Text, keresettKerdesek[0]);
                txbPont.Text = elertPont.ToString() + " pont";
                if (elertPont == 0) MessageBox.Show($"A helyes válasz {keresettKerdesek[0].Valasz} volt.");
            }
            else
            {
                Kerdes kerdes = keresettKerdesek.Find(x => x.MuveltsegiKerdes.Contains(txbSorsoltKerdes.Text));
                if (kerdes != null)
                {
                    int elertPont = ValaszPontozas(txbValasz.Text, kerdes);
                    txbPont.Text = elertPont.ToString() + " pont";
                    if (elertPont == 0) MessageBox.Show($"A helyes válasz {kerdes.Valasz} volt.");
                }
            }
        }

        private int ValaszPontozas(string valasz, Kerdes kerdes)
        {
            if (Convert.ToUInt32(kerdes.Valasz) == Convert.ToInt32(valasz))
            {
                return kerdes.Pont;
            }
            else return 0;
        }

        private void General_Click(object sender, RoutedEventArgs e)
        {
            List<Kerdes> Feladatsor = new List<Kerdes>();

            Random rnd = new Random();
            for (int i = 0; i < 15; i++)
            {
                int random = rnd.Next(0, kerdesek.Count);
                if (!Feladatsor.Contains(kerdesek[random]))
                {
                    Feladatsor.Add(kerdesek[random]);
                }
                else
                {
                    i--;
                }
            }

            using StreamWriter writer = new StreamWriter(@"../../../src/15_feladat.txt", false);
            writer.WriteLine(Feladatsor.Sum(x => x.Pont));
            foreach (Kerdes kerdes in Feladatsor)
            {
                writer.Write(kerdes.MuveltsegiKerdes + "@");
            }
        }
    }
}