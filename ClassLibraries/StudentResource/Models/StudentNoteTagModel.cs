﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentResource.Models;

public class StudentNoteTagModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int NoteId { get; set; }

}
