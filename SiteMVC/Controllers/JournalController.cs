﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiteMVC.Enums;
using SiteMVC.Models;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers
{
    public class JournalController : Controller
    {
        private readonly ApplicationContext applicationContext;
        private readonly JournalRepository journalRepository;
        private readonly UserRepository userRepository;
        private readonly LessonRepository lessonRepository;
        private readonly SubjectRepository subjectRepository;

        public JournalController(ApplicationContext applicationContext, JournalRepository journalRepository,
            UserRepository userRepository, LessonRepository lessonRepository, SubjectRepository subjectRepository)
        {
            this.applicationContext = applicationContext;
            this.journalRepository = journalRepository;
            this.userRepository = userRepository;
            this.lessonRepository = lessonRepository;
            this.subjectRepository = subjectRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await applicationContext.Journals.Include(j => j.Lesson).Include(j => j.User).Include(j => j.Subject).ToListAsync());
        }
        public async Task<IActionResult> Create()
        {
            IEnumerable<Users> students;
            students = await userRepository.GetStudents();
            ViewData["Lesson"] = new SelectList(applicationContext.Lessons, "Id", "LessonNumber");
            ViewData["Students"] = new SelectList(students, "Id", "FIO");
            ViewData["Subjects"] = new SelectList(applicationContext.Subjects, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DateTime date, int? grade, string? workType,
            int lessonId, int subjectId, int userId, string? presence)
        {
            var lesson = await lessonRepository.GetLessonByIdAsync(lessonId);
            var subject = await subjectRepository.GetSubjectByIdAsync(subjectId);
            var user = await userRepository.GetUserByIdAsync(userId);
            await journalRepository.AddNewJournal(date, grade, workType, lesson, subject, user, presence);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || applicationContext.Journals == null)
            {
                return NotFound();
            }

            var journal = await applicationContext.Journals
                .Include(j => j.Lesson)
                .Include(j => j.Subject)
                .Include(j => j.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (journal == null)
            {
                return NotFound();
            }
            IEnumerable<Users> students;
            students = await userRepository.GetStudents();
            ViewData["Lesson"] = new SelectList(applicationContext.Lessons, "Id", "LessonNumber", journal.LessonID);
            ViewData["Subject"] = new SelectList(applicationContext.Subjects, "Id", "Name", journal.SubjectId);
            ViewData["Students"] = new SelectList(students, "Id", "FIO", journal.UserID);
            return View(journal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Date, Grade, WorkType, LessonID, UserID, SubjectId")] Journal journal)
        {
            if (id != journal.Id)
            {
                return NotFound();
            }
            applicationContext.Update(journal);
            await applicationContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || applicationContext.Journals == null)
            {
                return NotFound();
            }

            var journal = await applicationContext.Journals
               .Include(j => j.Lesson)
               .Include(j => j.User)
               .Include(j => j.Subject)
               .FirstOrDefaultAsync(j => j.Id == id);
            if (journal == null)
            {
                return NotFound();
            }

            return View(journal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (applicationContext.Journals == null)
            {
                return Problem("Entity set 'applicationContext.Journals'  is null.");
            }
            var journal = await applicationContext.Journals
               .Include(j => j.Lesson)
               .Include(j => j.User)
               .FirstOrDefaultAsync(j => j.LessonID == id && j.UserID == id);

            if (journal != null)
            {
                applicationContext.Journals.Remove(journal);
            }

            await applicationContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
