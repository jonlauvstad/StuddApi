using StudentResource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentResource.Services;

public class StudentNoteService
{
    private readonly UserClient _userClient;
    private readonly CourseClient _courseClient;
    private readonly CourseImplementationClient _courseImplementationClient;

    public StudentNoteService(UserClient userClient, CourseClient courseClient, CourseImplementationClient courseImplementationClient)
    {
        _userClient = userClient;
        _courseClient = courseClient;
        _courseImplementationClient = courseImplementationClient;
    }

    public async Task<StudentNoteModel> CreateNoteAsync(StudentNoteModel note)
    {
        // Here you might want to fetch user or course info if needed
        // For example, to validate the user or course exists
        var user = await _userClient.GetUserByIdAsync(note.UserId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        // Similarly, fetch and validate course information if necessary

        // Then, save the note to your database or wherever you plan to store these notes

        return note; // Return the saved note
    }

    // Implement other methods for retrieving, updating, and deleting notes as needed
}
