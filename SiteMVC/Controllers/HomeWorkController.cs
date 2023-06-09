﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers
{
    public class HomeWorkController : Controller
    {
        private readonly ApplicationContext applicationContext;
        private readonly HomeWorkRepository homeWorkRepository;
        private readonly LessonRepository lessonRepository;
        private readonly ClassRepository classRepository;

        public HomeWorkController(ApplicationContext applicationContext, HomeWorkRepository homeWorkRepository,
            LessonRepository lessonRepository, ClassRepository classRepository)
        {
            this.applicationContext = applicationContext;
            this.homeWorkRepository = homeWorkRepository;
            this.lessonRepository = lessonRepository;
            this.classRepository = classRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await applicationContext.HomeWorks.Include(h => h.Lesson).Include(h => h.Class).ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Lesson"] = new SelectList(applicationContext.Lessons, "Id", "LessonNumber");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DateTime dateTime, string description, int? classId, int? lessonId)
        {
            var _class = await classRepository.GetClassByIdAsync(classId);
            var lesson = await lessonRepository.GetLessonByIdAsync(lessonId);
            await homeWorkRepository.AddNewHomeWork(dateTime, description, lesson, _class);
            return Redirect("~/HomeWork/Index");
        }
    }
}