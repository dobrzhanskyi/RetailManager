﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;
using RMDataManager.Models;

namespace RMDataManager.Controllers
{
	[Authorize]
	public class UserController : ApiController
	{
		[HttpGet]
		public UserModel GetById()
		{
			//Getting id from logged user
			string userId = RequestContext.Principal.Identity.GetUserId();
			UserData userData = new UserData();

			return userData.GetUserById(userId).First();
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		[Route("api/User/Admin/GetAllUsers")]
		public List<ApplicationUserModel> GetAllUsers()
		{
			List<ApplicationUserModel> output = new List<ApplicationUserModel>();

			using (var context = new ApplicationDbContext())
			{
				var userStore = new UserStore<ApplicationUser>(context);
				var userManager = new UserManager<ApplicationUser>(userStore);

				var users = userManager.Users.ToList();
				var roles = context.Roles.ToList();

				foreach (var user in users)
				{
					ApplicationUserModel u = new ApplicationUserModel()
					{
						Id = user.Id,
						Email = user.Email
					};
					foreach (var r in user.Roles)
					{
						u.Roles.Add(r.RoleId, roles.Where(x => x.Id == r.RoleId).First().Name);
					}

					output.Add(u);
				}
			}
			return output;
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		[Route("api/User/Admin/GetAllRoles")]
		public Dictionary<string, string> GetAllRoles()
		{
			using (var context = new ApplicationDbContext())
			{
				var roles = context.Roles.ToDictionary(x => x.Id, x => x.Name);

				return roles;
			}
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		[Route("api/User/Admin/AddRole")]
		public void AddRole(UserRolePairModel pairing)
		{
			using (var context = new ApplicationDbContext())
			{
				var userStore = new UserStore<ApplicationUser>(context);
				var userManager = new UserManager<ApplicationUser>(userStore);

				userManager.AddToRole(pairing.UserId, pairing.RoleName);
			}
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		[Route("api/User/Admin/RemoveRole")]
		public void RemoveRole(UserRolePairModel pairing)
		{
			using (var context = new ApplicationDbContext())
			{
				var userStore = new UserStore<ApplicationUser>(context);
				var userManager = new UserManager<ApplicationUser>(userStore);

				userManager.RemoveFromRole(pairing.UserId, pairing.RoleName);
			}
		}
	}
}