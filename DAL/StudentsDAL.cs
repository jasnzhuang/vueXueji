﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testxueji.Models;

namespace vuexueji.DAL
{

    public class StudentsDAL
    {
        //public static string SingleName(int id)
        //{
        //    using (var db = new XuejiContext())
        //    {
        //        var list = db.Studentses.SingleOrDefault(s => s.Id == id);
        //        if (list != null) return list.Name;
        //    }

        //    return null;
        //}

        public static StudentsClasses Single(int id)
        {
            using (var db = new XuejiContext())
            {
                var list = from s in db.Studentses
                           join c in db.Classeses on s.ClassesId equals c.Id
                           join m in db.Majorses on s.Classes.MajorsId equals m.Id
                           where s.Id == id
                           select new StudentsClasses()
                           {
                               id = s.Id,
                               number = s.Number,
                               name = s.Name,
                               sex = s.Sex,
                               age = s.Age,
                               phone = s.Phone,
                               qq = s.QQ,
                               wechat = s.Wechat,
                               status = s.Status,
                               classes = c.Year + m.Name,
                               classesId = c.Id
                           };
                return list.SingleOrDefault();
            }

        }

        public static IEnumerable<Students> StudentList(int id)
        {
            using (var db = new XuejiContext())
            {
                var list = from s in db.Studentses
                           where s.ClassesId == id
                           select s;
                return list;
            }
        }

        public static IEnumerable<StudentsClasses> List()
        {
            using (var db = new XuejiContext())
            {
                var list = from s in db.Studentses
                           join c in db.Classeses on s.ClassesId equals c.Id
                           join m in db.Majorses on s.Classes.MajorsId equals m.Id
                           where s.Status != "退学"
                           select new StudentsClasses()
                           {
                               id = s.Id,
                               number = s.Number,
                               name = s.Name,
                               sex = s.Sex,
                               age = s.Age,
                               phone = s.Phone,
                               qq = s.QQ,
                               wechat = s.Wechat,
                               status = s.Status,
                               classes = c.Year + m.Name,
                               classesId = c.Id

                           };

                return list.ToList();
            }
        }

        public static void Add(Students c)
        {
            using (var db = new XuejiContext())
            {
                var students = new Students
                {
                    Number = c.Number,
                    Name = c.Name,
                    Sex = c.Sex,
                    Age = c.Age,
                    Phone = c.Phone,
                    QQ = c.QQ,
                    Wechat = c.Wechat,
                    Status = "正常",
                    ClassesId = c.ClassesId
                };
                db.Studentses.Add(students);
                db.SaveChanges();
            }
        }

        public static void Edit(Students students, int id)
        {
            using (var db = new XuejiContext())
            {
                var single = db.Studentses.SingleOrDefault(s => s.Id == id);
                if (single != null)
                {
                    single.Number = students.Number;
                    single.Name = students.Name;
                    single.Sex = students.Sex;
                    single.Age = students.Age;
                    single.Phone = students.Phone;
                    single.QQ = students.QQ;
                    single.Wechat = students.Wechat;
                    single.Status = students.Status;
                    single.ClassesId = students.ClassesId;
                }

                db.SaveChanges();
            }
        }

        public static void Del(int id)
        {
            using (var db = new XuejiContext())
            {
                var del = db.Studentses.SingleOrDefault(s => s.Id == id);
                if (del != null) db.Studentses.Remove(del);
                db.SaveChanges();
            }
        }
    }
}
