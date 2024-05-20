using LMS.DataAccess.Data;
using LMS.Models;
using LMS.Utility;
using LMSWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMSWeb.Areas.Admin.Controllers
{
    public class SubjectOutlineController : Controller
    {
        public readonly ApplicationDbContext _db;
        public SubjectOutlineController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Index()
        {
            var subjectOutline = _db.SubjectOutline.Select(p => new SubjectOutlineViewModel
            {
                Id = p.Id,
                SubjectId = p.SubjectId,
                SubjectName=p.Subject.Name,
                Overview = p.Overview,
                Importance = p.Importance,
                Objectives = p.Objectives,
                FundamentalPrinciples = p.FundamentalPrinciples,
                CoreConcepts = p.CoreConcepts,
                RealWorldApplications = p.RealWorldApplications,
                Implications = p.Implications,
                StudyPath = p.StudyPath,
                KeyResearchers = p.KeyResearchers,
                MajorStudies = p.MajorStudies,
                ResearchMethodologies = p.ResearchMethodologies,
                Textbooks = p.Textbooks,
                OnlineResources = p.OnlineResources,
                AdditionalReferences = p.AdditionalReferences,
                GradingRubrics = p.GradingRubrics,
                Summary = p.Summary,
                FutureStudy = p.FutureStudy,              
                
            }).ToList();


            return View(subjectOutline);
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Create()
        {
            var subjectList = _db.Subject.ToList();
            ViewBag.SubjectList = new SelectList(subjectList, "Id", "Name");
            
            return View();
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public IActionResult Create(SubjectOutlineViewModel p)
        {
            if (ModelState.IsValid)
            {
                SubjectOutline subjectOutline = new SubjectOutline
                {
                    Id = Guid.NewGuid(),
                    SubjectId = p.SubjectId,
                    Overview = p.Overview,
                    Importance = p.Importance,
                    Objectives = p.Objectives,
                    FundamentalPrinciples = p.FundamentalPrinciples,
                    CoreConcepts = p.CoreConcepts,
                    RealWorldApplications = p.RealWorldApplications,
                    Implications = p.Implications,
                    StudyPath = p.StudyPath,
                    KeyResearchers = p.KeyResearchers,
                    MajorStudies = p.MajorStudies,
                    ResearchMethodologies = p.ResearchMethodologies,
                    Textbooks = p.Textbooks,
                    OnlineResources = p.OnlineResources,
                    AdditionalReferences = p.AdditionalReferences,
                    GradingRubrics = p.GradingRubrics,
                    Summary = p.Summary,
                    FutureStudy = p.FutureStudy,
                   
                };

                _db.SubjectOutline.Add(subjectOutline);

                _db.SaveChanges();
                TempData["success"] = "Subject Outline created successfully";
                return RedirectToAction("index");
            }
            else
            {
                var subjects = _db.Subject.ToList();
                ViewBag.SubjectList = new SelectList(subjects, "Id", "Name");
                return View();
            }

        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Update(Guid Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            SubjectOutline subjectOutline = _db.SubjectOutline.Find(Id);
            if (subjectOutline == null)
            {
                return NotFound();
            }

            var model = new SubjectOutlineViewModel
            {
                Id = subjectOutline.Id,
                SubjectId = subjectOutline.SubjectId,
                Overview = subjectOutline.Overview,
                Importance = subjectOutline.Importance,
                Objectives = subjectOutline.Objectives,
                FundamentalPrinciples = subjectOutline.FundamentalPrinciples,
                CoreConcepts = subjectOutline.CoreConcepts,
                RealWorldApplications = subjectOutline.RealWorldApplications,
                Implications = subjectOutline.Implications,
                StudyPath = subjectOutline.StudyPath,
                KeyResearchers = subjectOutline.KeyResearchers,
                MajorStudies = subjectOutline.MajorStudies,
                ResearchMethodologies = subjectOutline.ResearchMethodologies,
                Textbooks = subjectOutline.Textbooks,
                OnlineResources = subjectOutline.OnlineResources,
                AdditionalReferences = subjectOutline.AdditionalReferences,
                GradingRubrics = subjectOutline.GradingRubrics,
                Summary = subjectOutline.Summary,
                FutureStudy = subjectOutline.FutureStudy,
            };
            var subjectList = _db.Subject.ToList();
            ViewBag.SubjectList = new SelectList(subjectList, "Id", "Name");

            return View(model);
        }



        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public IActionResult Update(SubjectOutline obj)
        {

            if (obj == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {

                SubjectOutline subjectOutline = new SubjectOutline
                {
                    Id = obj.Id,
                    SubjectId = obj.SubjectId,
                    Overview = obj.Overview,
                    Importance = obj.Importance,
                    Objectives = obj.Objectives,
                    FundamentalPrinciples = obj.FundamentalPrinciples,
                    CoreConcepts = obj.CoreConcepts,
                    RealWorldApplications = obj.RealWorldApplications,
                    Implications = obj.Implications,
                    StudyPath = obj.StudyPath,
                    KeyResearchers = obj.KeyResearchers,
                    MajorStudies = obj.MajorStudies,
                    ResearchMethodologies = obj.ResearchMethodologies,
                    Textbooks = obj.Textbooks,
                    OnlineResources = obj.OnlineResources,
                    AdditionalReferences = obj.AdditionalReferences,
                    GradingRubrics = obj.GradingRubrics,
                    Summary = obj.Summary,
                    FutureStudy = obj.FutureStudy,
                };
                _db.SubjectOutline.Update(subjectOutline);
                _db.SaveChanges();
                TempData["success"] = "Subject Outline Updated successfully";
                return RedirectToAction("index");
            }
            else
            {
                var subjectList = _db.Subject.ToList();
                ViewBag.SubjectList = new SelectList(subjectList, "Id", "Name");

                return View();
            }
        }

        /// <summary>
        /// delete
        /// </summary>
        /// <returns></returns>

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Delete(Guid Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            SubjectOutline subjectOutline = _db.SubjectOutline.Find(Id);
            if (subjectOutline == null)
            {
                return NotFound();
            }

            var model = new SubjectOutlineViewModel
            {
                Id = subjectOutline.Id,
                SubjectId = subjectOutline.SubjectId,
                Overview = subjectOutline.Overview,
                Importance = subjectOutline.Importance,
                Objectives = subjectOutline.Objectives,
                FundamentalPrinciples = subjectOutline.FundamentalPrinciples,
                CoreConcepts = subjectOutline.CoreConcepts,
                RealWorldApplications = subjectOutline.RealWorldApplications,
                Implications = subjectOutline.Implications,
                StudyPath = subjectOutline.StudyPath,
                KeyResearchers = subjectOutline.KeyResearchers,
                MajorStudies = subjectOutline.MajorStudies,
                ResearchMethodologies = subjectOutline.ResearchMethodologies,
                Textbooks = subjectOutline.Textbooks,
                OnlineResources = subjectOutline.OnlineResources,
                AdditionalReferences = subjectOutline.AdditionalReferences,
                GradingRubrics = subjectOutline.GradingRubrics,
                Summary = subjectOutline.Summary,
                FutureStudy = subjectOutline.FutureStudy,
            };
            var subjectList = _db.Subject.ToList();
            ViewBag.SubjectList = new SelectList(subjectList, "Id", "Name");

            return View(model);
        }

        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(Guid Id)
        {
            SubjectOutline? subjectOutline = _db.SubjectOutline.Find(Id);
            if (subjectOutline == null)
            {
                return NotFound();
            }
            _db.SubjectOutline.Remove(subjectOutline);
            _db.SaveChanges();
            TempData["success"] = "Subject Outline deleted successfully";
            return RedirectToAction("index");
        }

    }
}
