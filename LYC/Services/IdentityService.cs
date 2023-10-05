using LYC.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Utilities;
using ViewModels.Identity;
using LYC.Models.Identity;
using ViewModels;
using Utilities.Enums;
using LYC.Models;
using static Utilities.Constants;

namespace LYC.Services
{
    public interface IIdentityService
    {
        Task<Response> AddNewRole(RoleVM model, UserJwtVM userJwt);
        Task<Response> AuthenticateUser(LoginVM model);
        Task<Response> RegisterUser(StaffVM model, UserJwtVM userJwt);
        Response GetRoles();
        Task<Response> GetAllUsers();
        Task<Response> DeleteRole(string roleId, UserJwtVM userJwt);
        Task<Response> DeleteStaff(string userId, UserJwtVM userJwt);
        Task<Response> UpdateUser(StaffVM staffVM, UserJwtVM userJwt);
        Task<Response> UpdateRole(RoleVM roleVM, UserJwtVM userJwt);
        Task<Response> GetAllCustomers();
        Task<Response> GetAllCustomersByBranchId(long branchId);
    }

    class IdentityService : IIdentityService
    {
        private readonly IConfiguration _configuration;

        private readonly IUnitOfWork _uow;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public IdentityService(IConfiguration configuration, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IUnitOfWork uow)
        {
            _uow = uow;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<Response> AuthenticateUser(LoginVM model)
        {
            Response response;

            try
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user != null && !user.Deleted)
                {
                    if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);

                        var authClaims = new List<Claim>
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(ClaimTypes.NameIdentifier, user.Id),
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.Email, user.Email)
                        };

                        foreach (var userRole in userRoles)
                        {
                            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                        }

                        var token = JWTHelper.GetToken(authClaims, _configuration);

                        response = new Response()
                        {
                            ResultData = new JwtSecurityTokenHandler().WriteToken(token),
                            Status = ResponseStatus.OK,
                            Message = ResponseMessages.Successfull
                        };
                    }
                    else
                    {
                        response = new Response()
                        {
                            ResultData = null,
                            Status = ResponseStatus.Restrected,
                            Message = ResponseMessages.UserNotFound
                        };
                    }
                }
                else
                {
                    response = new Response()
                    {
                        Message = ResponseMessages.AccountDeleted,
                        ResultData = null,
                        Status = ResponseStatus.Error
                    };
                }
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message,
                    ResultData = null,
                    Status = ResponseStatus.Error
                };
            }

            return response;
        }

        public async Task<Response> RegisterUser(StaffVM model, UserJwtVM userJwt)
        {
            Response response;

            try
            {
                var userExists = await _userManager.FindByNameAsync(model.UserName);

                if (userExists != null)
                {
                    response = new Response()
                    {
                        ResultData = null,
                        Status = ResponseStatus.Restrected,
                        Message = ResponseMessages.UserAlreadyExist
                    };
                }
                else
                {
                    ApplicationUser user = new()
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        UserDepartment = model.Department,
                        CreatedBy = userJwt.UserId,
                        CreatedOn = DateTime.Now,
                        Contact = model.Contact
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (!result.Succeeded)
                    {
                        response = new Response()
                        {
                            ResultData = null,
                            Status = ResponseStatus.Restrected,
                            Message = result.Errors.FirstOrDefault()?.Description,
                        };
                    }
                    else
                    {
                        if (!await _roleManager.RoleExistsAsync(model.UserRole))
                        {
                            await _roleManager.CreateAsync(new ApplicationRole(model.UserRole)
                            {
                                CreatedBy = userJwt.UserId,
                                CreatedOn = DateTime.Now
                            });
                        }

                        if (await _roleManager.RoleExistsAsync(model.UserRole))
                        {
                            await _userManager.AddToRoleAsync(user, model.UserRole);
                        }

                        if (model.BranchIds.Count > 0)
                        {
                            StaffBranchAssociation staffBranchAssociation;

                            foreach (var branchId in model.BranchIds)
                            {
                                staffBranchAssociation = new StaffBranchAssociation
                                {
                                    BranchId = branchId,
                                    UserId = user.Id,
                                    CreatedBy = userJwt.UserId,
                                    CreatedOn = DateTime.Now
                                };

                                _uow.Repository<StaffBranchAssociation>().Add(staffBranchAssociation);
                            }

                            _uow.Save();
                        }

                        response = new Response()
                        {
                            Message = ResponseMessages.Successfull,
                            Status = ResponseStatus.OK
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message,
                    ResultData = ex.InnerException,
                    Status = ResponseStatus.Error
                };
            }

            return response;
        }

        public async Task<Response> AddNewRole(RoleVM model, UserJwtVM userJwt)
        {
            Response response;

            try
            {

                if (model != null)
                {
                    if (!await _roleManager.RoleExistsAsync(model.RoleName))
                    {
                        await _roleManager.CreateAsync(new ApplicationRole(model.RoleName)
                        {
                            CreatedBy = userJwt.UserId,
                            CreatedOn = DateTime.Now
                        });

                        response = new Response()
                        {
                            ResultData = model.RoleName,
                            Message = ResponseMessages.Successfull,
                            Status = ResponseStatus.OK
                        };
                    }
                    else
                    {
                        response = new Response()
                        {
                            ResultData = null,
                            Status = ResponseStatus.Restrected,
                            Message = ResponseMessages.AlreadyExist
                        };
                    }
                }
                else
                {
                    response = new Response()
                    {
                        ResultData = null,
                        Status = ResponseStatus.Restrected,
                        Message = ResponseMessages.DataNotFound
                    };
                }
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message,
                    ResultData = ex.InnerException,
                    Status = ResponseStatus.Error
                };
            }

            return response;
        }

        public Response GetRoles()
        {
            Response response;

            try
            {
                var roles = _roleManager.Roles.Where(x => x.Deleted == false).OrderByDescending(x => x.CreatedOn).ToList();

                if (roles?.Count > 0)
                {
                    response = new Response()
                    {
                        ResultData = roles,
                        Message = ResponseMessages.Successfull,
                        Status = ResponseStatus.OK
                    };
                }
                else
                {
                    response = new Response()
                    {
                        ResultData = null,
                        Status = ResponseStatus.Restrected,
                        Message = ResponseMessages.DataNotFound
                    };
                }
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message,
                    ResultData = ex.InnerException,
                    Status = ResponseStatus.Error
                };
            }

            return response;
        }

        public async Task<Response> GetAllUsers()
        {
            Response response;

            List<StaffVM> staffs;

            try
            {
                var applicationUsers = _userManager.Users.Where(x => x.Deleted == false).OrderByDescending(x => x.CreatedOn).ToList();

                if (applicationUsers?.Count > 0)
                {
                    staffs = new List<StaffVM>();

                    foreach (var user in applicationUsers)
                    {
                        var roles = await _userManager.GetRolesAsync(user);

                        var staffVM = new StaffVM
                        {
                            UserId = user.Id,
                            Email = user.Email,
                            //StaffId = user.StaffId,
                            UserName = user.UserName,
                            Password = user.PasswordHash,
                            UserRole = roles.ToList().FirstOrDefault(),
                            Department = user.UserDepartment,
                            Contact = user.Contact
                        };

                        staffs.Add(staffVM);
                    }

                    if (staffs.Count > 0)
                    {
                        response = new Response()
                        {
                            ResultData = staffs,
                            Message = ResponseMessages.Successfull,
                            Status = ResponseStatus.OK
                        };
                    }
                    else
                    {
                        response = new Response()
                        {
                            ResultData = null,
                            Status = ResponseStatus.Restrected,
                            Message = ResponseMessages.DataNotFound
                        };
                    }
                }
                else
                {
                    response = new Response()
                    {
                        ResultData = null,
                        Status = ResponseStatus.Restrected,
                        Message = ResponseMessages.DataNotFound
                    };
                }
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message,
                    ResultData = ex.InnerException,
                    Status = ResponseStatus.Error
                };
            }

            return response;
        }

        public async Task<Response> GetAllCustomers()
        {
            Response response;

            try
            {
                List<UserVM> userVMs = new List<UserVM>();

                var applicationUsers = _userManager.Users.Where(x => x.Deleted == false).OrderByDescending(x => x.CreatedOn).ToList();

                if (applicationUsers?.Count > 0)
                {
                    foreach (var user in applicationUsers)
                    {
                        var roles = await _userManager.GetRolesAsync(user);

                        if (roles.ToList().FirstOrDefault() == UserRoles.User || roles.ToList().FirstOrDefault() == UserRoles.User)
                        {
                            var userVM = new UserVM
                            {
                                UserId = user.Id,
                                UserEmail = user.Email,
                                UserName = user.UserName
                            };

                            userVMs.Add(userVM);
                        }
                    }

                    if (userVMs.Count > 0)
                    {
                        response = new Response()
                        {
                            ResultData = userVMs,
                            Message = ResponseMessages.Successfull,
                            Status = ResponseStatus.OK
                        };
                    }
                    else
                    {
                        response = new Response()
                        {
                            ResultData = null,
                            Status = ResponseStatus.Restrected,
                            Message = ResponseMessages.DataNotFound
                        };
                    }
                }
                else
                {
                    response = new Response()
                    {
                        ResultData = null,
                        Status = ResponseStatus.Restrected,
                        Message = ResponseMessages.DataNotFound
                    };
                }
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message,
                    ResultData = ex.InnerException,
                    Status = ResponseStatus.Error
                };
            }

            return response;
        }

        public async Task<Response> GetAllCustomersByBranchId(long branchId)
        {
            Response response;

            try
            {
                List<UserVM> userVMs = new List<UserVM>();

                var customersIdsInBranchList = _uow.Repository<StaffBranchAssociation>().GetAll().Where(x => x.BranchId == branchId).Select(x => x.UserId).ToList();

                var applicationUserList = _userManager.Users.Where(x => x.Deleted == false).OrderByDescending(x => x.CreatedOn);

                var applicationUsers = applicationUserList.Where(x => customersIdsInBranchList.Contains(x.Id)).ToList();

                if (applicationUsers?.Count > 0)
                {
                    foreach (var user in applicationUsers)
                    {
                        var roles = await _userManager.GetRolesAsync(user);

                        if (roles.ToList().FirstOrDefault() == UserRoles.User || roles.ToList().FirstOrDefault() == UserRoles.Customer)
                        {
                            var userVM = new UserVM
                            {
                                UserId = user.Id,
                                UserEmail = user.Email,
                                UserName = user.UserName
                            };

                            userVMs.Add(userVM);
                        }
                    }

                    if (userVMs.Count > 0)
                    {
                        response = new Response()
                        {
                            ResultData = userVMs,
                            Message = ResponseMessages.Successfull,
                            Status = ResponseStatus.OK
                        };
                    }
                    else
                    {
                        response = new Response()
                        {
                            ResultData = null,
                            Status = ResponseStatus.Restrected,
                            Message = ResponseMessages.DataNotFound
                        };
                    }
                }
                else
                {
                    response = new Response()
                    {
                        ResultData = null,
                        Status = ResponseStatus.Restrected,
                        Message = ResponseMessages.DataNotFound
                    };
                }
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message,
                    ResultData = ex.InnerException,
                    Status = ResponseStatus.Error
                };
            }

            return response;
        }

        public async Task<Response> DeleteRole(string roleId, UserJwtVM userJwt)
        {
            Response response;

            try
            {
                var role = await _roleManager.FindByIdAsync(roleId);

                if (role != null)
                {
                    role.Deleted = true;
                    role.ModifiedBy = userJwt.UserId;
                    role.ModifiedOn = DateTime.Now;

                    var result = await _roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                    {
                        response = new Response()
                        {
                            ResultData = null,
                            Message = ResponseMessages.Successfull,
                            Status = ResponseStatus.OK
                        };
                    }
                    else
                    {
                        response = new Response()
                        {
                            ResultData = result.Errors,
                            Message = ResponseMessages.UnSuccessfull,
                            Status = ResponseStatus.OK
                        };
                    }
                }
                else
                {
                    response = new Response()
                    {
                        ResultData = null,
                        Status = ResponseStatus.Restrected,
                        Message = ResponseMessages.DataNotFound
                    };
                }
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message,
                    ResultData = ex.InnerException,
                    Status = ResponseStatus.Error
                };
            }

            return response;
        }

        public async Task<Response> DeleteStaff(string userId, UserJwtVM userJwt)
        {
            Response response;

            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    user.Deleted = true;
                    user.ModifiedBy = userJwt.UserId;
                    user.ModifiedOn = DateTime.Now;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        response = new Response()
                        {
                            ResultData = null,
                            Message = ResponseMessages.Successfull,
                            Status = ResponseStatus.OK
                        };
                    }
                    else
                    {
                        response = new Response()
                        {
                            ResultData = result.Errors,
                            Message = ResponseMessages.UnSuccessfull,
                            Status = ResponseStatus.OK
                        };
                    }
                }
                else
                {
                    response = new Response()
                    {
                        ResultData = null,
                        Status = ResponseStatus.Restrected,
                        Message = ResponseMessages.DataNotFound
                    };
                }
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message,
                    ResultData = ex.InnerException,
                    Status = ResponseStatus.Error
                };
            }

            return response;
        }

        public async Task<Response> UpdateUser(StaffVM model, UserJwtVM userJwt)
        {
            Response response;

            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId);

                if (user != null)
                {
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.Contact = model.Contact;
                    user.UserDepartment = model.Department;
                    user.ModifiedBy = userJwt.UserId;
                    user.ModifiedOn = DateTime.Now;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                        if (!string.Equals(model.UserRole, userRole))
                        {
                            if (!await _roleManager.RoleExistsAsync(model.UserRole))
                            {
                                await _roleManager.CreateAsync(new ApplicationRole(model.UserRole)
                                {
                                    CreatedBy = userJwt.UserId,
                                    CreatedOn = DateTime.Now
                                });
                            }

                            if (await _roleManager.RoleExistsAsync(model.UserRole))
                            {
                                await _userManager.RemoveFromRoleAsync(user, userRole);
                                await _userManager.AddToRoleAsync(user, model.UserRole);
                            }
                        }

                        response = new Response()
                        {
                            ResultData = null,
                            Message = ResponseMessages.Successfull,
                            Status = ResponseStatus.OK
                        };
                    }
                    else
                    {
                        response = new Response()
                        {
                            ResultData = result.Errors,
                            Message = ResponseMessages.UnSuccessfull,
                            Status = ResponseStatus.OK
                        };
                    }
                }
                else
                {
                    response = new Response()
                    {
                        ResultData = null,
                        Status = ResponseStatus.Restrected,
                        Message = ResponseMessages.DataNotFound
                    };
                }
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message,
                    ResultData = ex.InnerException,
                    Status = ResponseStatus.Error
                };
            }

            return response;
        }

        public async Task<Response> UpdateRole(RoleVM roleVM, UserJwtVM userJwt)
        {
            Response response;

            try
            {
                var role = await _roleManager.FindByIdAsync(roleVM.Id);

                if (role != null)
                {
                    role.Name = roleVM.RoleName;
                    role.ModifiedBy = userJwt.UserId;
                    role.ModifiedOn = DateTime.Now;

                    var result = await _roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                    {
                        response = new Response()
                        {
                            ResultData = null,
                            Message = ResponseMessages.Successfull,
                            Status = ResponseStatus.OK
                        };
                    }
                    else
                    {
                        response = new Response()
                        {
                            ResultData = result.Errors,
                            Message = ResponseMessages.UnSuccessfull,
                            Status = ResponseStatus.OK
                        };
                    }
                }
                else
                {
                    response = new Response()
                    {
                        ResultData = null,
                        Status = ResponseStatus.Restrected,
                        Message = ResponseMessages.DataNotFound
                    };
                }
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message,
                    ResultData = ex.InnerException,
                    Status = ResponseStatus.Error
                };
            }

            return response;
        }
    }
}