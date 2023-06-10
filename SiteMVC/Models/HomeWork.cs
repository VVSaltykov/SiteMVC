using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteMVC.Models
{
    public class HomeWork
    {
        public int Id { get; set; }
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        [Display(Name = "Задание")]
        public string? Description { get; set; }
        public int? ClassID { get; set; }
        public int? LessonId { get; set; }
        [ForeignKey("ClassID")]
        public Class? Class { get; set; }
        [ForeignKey("LessonId")]
        public Lesson? Lesson { get; set; }
    }
}
