using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace GUI_2_LAB
{
	/// <summary>
	/// Interaction logic for QuizWindow.xaml
	/// </summary>
	public partial class QuizWindow : Window
	{
		DispatcherTimer _timer;
		TimeSpan _time;
		private Quiz q;
		private string selected;
		public QuizWindow(Quiz q)
		{
			InitializeComponent();

			this.q = q;

			int i = 0;
			tb_question.Text = q.Question;
			foreach (var item in q.Options)
			{
				(grid_Answers.Children[i] as Label).Content += item;
				i++;
			}
		}

		private void bt_submit_Click(object sender, RoutedEventArgs e)
		{
			
			if (selected==q.Answer)
			{
				this.DialogResult = true;
			}
			else
			{
				this.DialogResult = false;
			}
		}

		private void bt_submit_MouseEnter(object sender, MouseEventArgs e)
		{
			this.bt_submit.Background = new SolidColorBrush(Color.FromRgb(66, 69, 73));
		}

		private void bt_submit_MouseLeave(object sender, MouseEventArgs e)
		{
			this.bt_submit.Background = new SolidColorBrush(Color.FromRgb(54, 57, 62));
		}

		private void l_MouseEnter(object sender, MouseEventArgs e)
		{
			if ((e.Source as Label).IsEnabled == true)
			{
				(e.Source as Label).Background = new SolidColorBrush(Color.FromRgb(66, 69, 73));
			}
		}
		private void l_MouseLeave(object sender, MouseEventArgs e)
		{
			if ((e.Source as Label).IsEnabled==true)
			{
				(e.Source as Label).Background = new SolidColorBrush(Color.FromRgb(54, 57, 62));
			}
		}

		private void l_MouseClick(object sender, MouseButtonEventArgs e)
		{
			foreach (var item in grid_Answers.Children)
			{
				(item as Label).IsEnabled = true;
				(item as Label).Background = new SolidColorBrush(Color.FromRgb(54, 57, 62));
			}

			(e.Source as Label).IsEnabled = false;

			selected = (e.Source as Label).Content.ToString().Split(": ")[1];
			(e.Source as Label).Background = new SolidColorBrush(Color.FromRgb(66, 69, 73));
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

				_time = TimeSpan.FromSeconds(20);

				_timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
				{
					tb_timer.Text = _time.TotalSeconds.ToString();
					if (_time == TimeSpan.Zero)
					{
						_timer.Stop();
						this.DialogResult = false;
					}
					if (_time==TimeSpan.FromSeconds(10))
					{
						tb_timer.Background = Brushes.DarkRed;
					}
					_time = _time.Add(TimeSpan.FromSeconds(-1));
				}, Application.Current.Dispatcher);

				_timer.Start();

		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			MessageBoxResult closing = MessageBox.Show("Biztos vagy benne? Nincs visszalépés.", "Kérdés", MessageBoxButton.YesNo);

			if (closing == MessageBoxResult.No)
			{
				e.Cancel = true;
			}
			else
			{
				_timer.Stop();
			}
		}
	}
}
