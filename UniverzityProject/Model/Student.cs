using System;
using System.Collections.Generic;

namespace UniverzityProject.Model;

public partial class Student
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public int? Age { get; set; }

    public string? Email { get; set; }

    public int? DepartmentId { get; set; }

    public bool? Enrolled { get; set; }
}
