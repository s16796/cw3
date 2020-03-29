﻿using System;
using System.Collections.Generic;
using System.Linq;
using cw3.DAL;
using cw3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cw3.Controllers
{
    [ApiController]
    [Route("api/students")]

    public class StudentsController : ControllerBase
    {

        private readonly IDbService _dbService;

        public StudentsController(IDbService dbservice)
        {
            _dbService = dbservice;
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            
            int numberofstudents = _dbService.getStudents().Count();
            IEnumerator<Student> enumerator = _dbService.getStudents().GetEnumerator();
            for (int i = 0; i <= numberofstudents; i++)
            {
                if (enumerator.Current != null && enumerator.Current.IdStudent == id)
                {
                    return Ok(enumerator.Current);
                }

                enumerator.MoveNext();
            }
            enumerator.Dispose();
            return Ok("Student not found");
        }

        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {
            return Ok(_dbService.getStudents());
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.indexNumber = $"s{new Random().Next(1, 99999)}";
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult PutStudent(int id, Student student)
        {
            return Ok("Aktualizacja studenta nr " + id + " dokończona.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok("Usuwanie studenta nr " +id+ " ukończone.");
        }

    }
}