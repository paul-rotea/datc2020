using System.Collections.Generic;
namespace students_api
{
public static class StudentRepo{
    public static List<Student> Students = new List<Student>(){
        new Student() {Id = 1, Name = "John", Faculty = "AC", StudyYear = 1},
        new Student() {Id = 2, Name = "Ion", Faculty = "ETC", StudyYear = 3}

    };


}

}