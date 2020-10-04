using System.Collections.Generic;
namespace student_api
{
public static class StudentRepo{
    public static List<Student> Students = new List<Student>(){
        new Student() {Id = 1, Name = "John", Faculty = "AC", StudyYear = 1},
        new Student() {Id = 2, Name = "Ion", Faculty = "ETC", StudyYear = 3},
        new Student() {Id = 3, Name = "Vasile", Faculty = "EE", StudyYear = 2},
        new Student() {Id = 4, Name = "Johnny", Faculty = "AC", StudyYear = 4},
        new Student() {Id = 5, Name = "Ionica", Faculty = "ETC", StudyYear = 1},
        new Student() {Id = 6, Name = "Vasilache", Faculty = "EE", StudyYear = 2}

    };


}

}