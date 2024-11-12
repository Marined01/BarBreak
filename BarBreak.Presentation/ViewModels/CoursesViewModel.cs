using System;
using System.Collections.ObjectModel;

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

        public CoursesViewModel()
        {
            // Заповнення списку курсів тестовими даними
            Courses = new ObservableCollection<Course>
            {
                new Course { Name = "Course 1", Description = "Description for Course 1", Image = "image1.png", IsFavorite = true },
                new Course { Name = "Course 2", Description = "Description for Course 2", Image = "image2.png", IsFavorite = false },
                // Додати інші курси за потребою
            };
        }
    }
}
