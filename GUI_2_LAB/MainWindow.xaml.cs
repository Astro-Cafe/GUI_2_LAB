using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI_2_LAB
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			List<Quiz> quizList = File.ReadAllLines("example.txt")
				.Select(x => new Quiz(x.Split(":")[0], x.Split(":")[1], new string[] { x.Split(":")[1], x.Split(":")[2], x.Split(":")[3], x.Split(":")[4] })).ToList();
			

			int i = 0;
			quizList.ForEach(quiz =>
			{
				Label l = new Label();
				l.Tag = quiz;
				l.Content = i++;
				l.HorizontalContentAlignment = HorizontalAlignment.Center;
				l.VerticalContentAlignment = VerticalAlignment.Center;
				l.Foreground = Brushes.Gainsboro;
				l.Background = new SolidColorBrush(Color.FromRgb(54, 57, 62));
				l.Margin = new Thickness(10);
				l.Height = this.ActualHeight / 3;
				l.Width = this.ActualWidth / 6;
				l.FontSize = 36;
				wrap.Children.Add(l);

				l.MouseLeftButtonDown += L_MouseLeftButtonDown;
				l.MouseMove += L_MouseMove;
				l.MouseLeave += L_MouseLeave;
			});
		}

		private void L_MouseLeave(object sender, MouseEventArgs e)
		{
			if (e.Source is Label)
			{
				(e.Source as Label).Background = new SolidColorBrush(Color.FromRgb(54, 57, 62));
			}
		}

		private void L_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Source is Label)
			{
				(e.Source as Label).Background = new SolidColorBrush(Color.FromRgb(66, 69, 73));
			}
		}

		private void L_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Quiz q = (Quiz)(sender as Label).Tag;

			QuizWindow qw = new QuizWindow(q);
			if (qw.ShowDialog()==true)
			{
				(sender as Label).Background = Brushes.LightGreen;
				(sender as Label).IsEnabled = false;
				//tb_correct.Text = (int.Parse(tb_correct.Text)+1).ToString();
			}
			else
			{
				(sender as Label).Background = Brushes.DarkRed;
				(sender as Label).IsEnabled = false;
				//tb_incorrect.Text = (int.Parse(tb_incorrect.Text) + 1).ToString();
			}
			
		}
	}

	public class Quiz
	{
		private string question;
		private string answer;
		private string[] options;
		public Quiz(string question,string answer,string[] option)
		{
			this.question = question;
			this.answer = answer;
			this.options = option;
			options.Shuffle<string>();
		}

		public string Question { get => question; set => question = value; }
		public string Answer { get => answer; set => answer = value; }
		public string[] Options { get => options; set => options = value; }
	}

	public static class ShuffleExtenstions
	{
		private static Random rng = new Random();
		public static void Shuffle<T>(this T[] array)
		{
			rng = new Random();
			int n = array.Length;
			while (n > 1)
			{
				int k = rng.Next(n);
				n--;
				T temp = array[n];
				array[n] = array[k];
				array[k] = temp;
			}
		}
	}
}
