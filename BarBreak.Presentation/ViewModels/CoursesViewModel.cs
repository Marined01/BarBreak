using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BarBreak.Presentation.ViewModels
{
    public class Course
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; } // Наприклад, URL або шлях до зображення
        public bool IsFavorite { get; set; }
    }

    public class CoursesViewModel
    {
        public ObservableCollection<Course> Courses { get; set; }
        public ICommand ToggleFavoriteCommand { get; }

        public CoursesViewModel()
        {
            Courses = new ObservableCollection<Course>
            {
                new Course { Name = "Course 1", Description = "Description for Course 1", Image = "image1.png", IsFavorite = true },
                new Course { Name = "Course 2", Description = "Description for Course 2", Image = "image2.png", IsFavorite = false },
                // Додати інші курси за потребою
            };

            ToggleFavoriteCommand = new RelayCommand(ToggleFavorite);
        }

        private void ToggleFavorite(object courseObj)
        {
            if (courseObj is Course course)
            {
                course.IsFavorite = !course.IsFavorite;
            }
        }
    }
}
