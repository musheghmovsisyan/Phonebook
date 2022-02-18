using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Core.Contracts.Services;
using PhoneBook.Models;

namespace PhoneBook.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly IMapper _mapper;
    private readonly IUserProfileServices _profileServices;

    private readonly long UserId = 1;

    public UserController(ILogger<UserController> logger, IMapper mapper, IUserProfileServices profileServices)
    {
        _logger = logger;
        _mapper = mapper;
        _profileServices = profileServices;
    }

    // GET: UserProfiles/Edit/5
    public async Task<IActionResult> UserProfile(long? userId)
    {
        DisplayMessage();

        var userProfileResult = await _profileServices.GetAsync(UserId);

        var userProfile = _mapper.Map<UserProfile>(userProfileResult) ?? new UserProfile
        {
            UserId = UserId
        };

        return View(userProfile);
    }

    [HttpPost]
    public ActionResult Cancel(long userId)
    {
        ClearMessage();

        return RedirectToAction("UserProfile", "User", new {UserId = userId});
    }

    [HttpPost]
    public ActionResult Delete(long userId, long id)
    {
        TempData["StatusMessage"] = "Deleted";
        TempData["LabelColor"] = "green";

        var isDeleted = _profileServices.Delete(id, userId);

        if (isDeleted)
        {
            SetInfoMessage("Successfully deleted.");
        }
        else
        {
            SetErrorMessage("Failed to delete.");
        }

        return RedirectToAction("UserProfile", "User", new {UserId = userId});
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UserProfile(UserProfile userProfile)
    {
        var isUserEmailsDeleted = userProfile.UserEmails.Any(_ => _.DeleteEmail);
        var isUserPhoneNumbersDeletedCount = userProfile.UserPhoneNumbers.Any(_ => _.DeletePhone);

        var userEmailsDeleteItems = userProfile.UserEmails.Where(_ => _.DeleteEmail && _.Id != 0)
            .Select(_ => new DeleteItem {Id = _.Id}).ToList() ?? new List<DeleteItem>();
        var userPhonesDeleteItems = userProfile.UserPhoneNumbers.Where(_ => _.DeletePhone && _.Id != 0)
            .Select(_ => new DeleteItem {Id = _.Id}).ToList() ?? new List<DeleteItem>();

        userProfile.UserEmailsDeleted = userProfile.UserEmailsDeleted.Union(userEmailsDeleteItems).ToList();
        userProfile.UserPhoneNumbersDeleted = userProfile.UserPhoneNumbersDeleted.Union(userPhonesDeleteItems).ToList();

        userProfile.UserEmails = userProfile.UserEmails.Where(_ => !_.DeleteEmail).ToList();
        userProfile.UserPhoneNumbers = userProfile.UserPhoneNumbers.Where(_ => !_.DeletePhone).ToList();

        if (isUserEmailsDeleted || isUserPhoneNumbersDeletedCount)
        {
            ModelState.Clear();
        }

        DisplayMessage();

        if (TryValidateModel(userProfile, nameof(Models.UserProfile)) && ModelState.IsValid)
        {
            var userProfileDto = _mapper.Map<Core.Entity.UserProfile>(userProfile);

            var userEmailsDeletedIds = userProfile.UserEmailsDeleted.Select(_ => _.Id).ToList();

            var userPhoneNumbersDeletedIds = userProfile.UserPhoneNumbersDeleted.Select(_ => _.Id).ToList();

            var userEmails = userProfileDto.UserEmails;
            var userPhoneNumbers = userProfileDto.UserPhoneNumbers;

            if (userProfileDto.Id > 0)
            {
                foreach (var userEmail in userEmails) userEmail.UserProfileId = userProfileDto.Id;
                foreach (var userPhoneNumber in userPhoneNumbers) userPhoneNumber.UserProfileId = userProfileDto.Id;

                if (userEmailsDeletedIds.Count > 0)
                {
                    _profileServices.DeleteUserEmails(userEmailsDeletedIds);
                }

                if (userPhoneNumbersDeletedIds.Count > 0)
                {
                    _profileServices.DeleteUserPhoneNumbers(userPhoneNumbersDeletedIds);
                }
            }

            var isUpdated = _profileServices.Update(userProfileDto);

            if (isUpdated)
            {
                SetInfoMessage("Saved.");
            }
            else
            {
                SetErrorMessage("Failed to save.");
            }

            return RedirectToAction("UserProfile", "User");
        }

        SetErrorMessage("Check Input data.");

        return View(userProfile);
    }

    public ActionResult NewUserEmail(int index)
    {
        ViewBag.EmailIndex = index;
        var email = new UserEmail {Email = null};
        return View("Partial/NewUserEmail", email);
    }

    public ActionResult NewUserPhoneNumber(int index)
    {
        ViewBag.PhoneNumberIndex = index;
        var phoneNumber = new UserPhoneNumber {PhoneNumber = null};
        return View("Partial/NewUserPhoneNumber", phoneNumber);
    }

    private void SetErrorMessage(string message)
    {
        TempData["StatusMessage"] = message;
        TempData["LabelColor"] = "red";
    }

    private void SetInfoMessage(string message)
    {
        TempData["StatusMessage"] = message;
        TempData["LabelColor"] = "green";
    }

    private void ClearMessage()
    {
        TempData["StatusMessage"] = null;
        TempData["LabelColor"] = null;
    }

    private void DisplayMessage()
    {
        if (TempData["StatusMessage"] is not null)
        {
            ViewData["StatusMessage"] = TempData["StatusMessage"];
            TempData["StatusMessage"] = null;
        }

        if (TempData["LabelColor"] is not null)
        {
            ViewData["LabelColor"] = TempData["LabelColor"];
            TempData["LabelColor"] = null;
        }
    }
}