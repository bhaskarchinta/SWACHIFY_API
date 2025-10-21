using System.ComponentModel.DataAnnotations;

namespace Swachify.Application;

public record UserCommandDto
(
    string first_name,
    string last_name,
    string email,
    string mobile,
    string password,
    long role_id
);

public record EmpCommandDto
(
    string first_name,
    string last_name,
    string email,
    string mobile,
    string location,
    string services
);