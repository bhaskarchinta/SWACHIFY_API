using System.ComponentModel.DataAnnotations;

namespace Swachify.Application;

public record UserCommandDto
(
    string first_name,
    string last_name,
    string email,
    string mobile,
    string password
);
