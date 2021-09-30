using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Common.Enums
{
    /// <summary>
    /// Response messages
    /// </summary>
    public static class ResponseStatus
    {
        /// <summary>
        /// The infomation is not available
        /// </summary>
        public static string NA = "N/A";

        /// <summary>
        /// The message sent
        /// </summary>
        public static string MessageSent = "The message has been sent successfully.";

        /// <summary>
        /// The message not sent
        /// </summary>
        public static string MessageNotSent = "Message not sent, please try sending again.";

        /// <summary>
        /// The success
        /// </summary>
        public static string success = "Success";

        /// <summary>
        /// The fail
        /// </summary>
        public static string fail = "Fail";

        /// <summary>
        /// The error
        /// </summary>
        public static string error = "Something went wrong please try after some time";

        /// <summary>
        /// The not found
        /// </summary>
        public static string notFound = "No record found";

        /// <summary>
        /// The user not found
        /// </summary>
        public static string userNotFound = "Either email or password is wrong.";

        /// <summary>
        /// The email exists
        /// </summary>
        public static string EmailExists = "This email already registered.";

        /// <summary>
        /// The phone exists
        /// </summary>
        public static string PhoneExists = "This phone number already registered.";

        /// <summary>
        /// The activated
        /// </summary>
        public static string Activated = "Image has been activated";

        /// <summary>
        /// The deactivated
        /// </summary>
        public static string Deactivated = "Image has been deactivated";

        /// <summary>
        /// The updated
        /// </summary>
        public static string Updated = "Image has been updated successfully";

        /// <summary>
        /// The item image added
        /// </summary>
        public static string ItemImageAdded = "Image has been Added";

        /// <summary>
        /// The update password
        /// </summary>
        public static string UpdatePassword = "Password updated successfully";

        /// <summary>
        /// The invalid token
        /// </summary>
        public static string InvalidToken = "Invalid Token";

        /// <summary>
        /// The reject job success
        /// </summary>
        public static string RejectJobSuccess = "Job rejected successfully";

        /// <summary>
        /// The reject job failed
        /// </summary>
        public static string RejectJobFailed = "Reject Job failed";

        /// <summary>
        /// The delete success
        /// </summary>
        public static string DeleteSuccess = "Deleted successfully";

        /// <summary>
        /// The invalid email credentials
        /// </summary>
        public static string InvalidEmailCredentials = "No user found with this email.";

        /// <summary>
        /// The invalid credentials
        /// </summary>
        public static string InvalidCredentials = "Either email or password is wrong.";

        public static string AccountDeactivated = "Your account has been deactivated by admin";

        /// <summary>
        /// The email not varified
        /// </summary>
        public static string EmailNotVarified = "Your account is not verified,please contact to admin";

        public static string EmailNotVerifiedByUser = "Email not confirmed yet";

        /// <summary>
        /// The invalid password
        /// </summary>
        public static string InvalidPassword = "Your password is wrong.";

        /// <summary>
        /// wrong old password
        /// </summary>
        public static string WrongOldPassword = "Wrong old password.";

        /// <summary>
        /// The validation failed
        /// </summary>
        public static string ValidationFailed = "Your request is not valid";

        /// <summary>
        /// The reset password link sent success
        /// </summary>
        public static string ResetPasswordLinkSentSuccess = "Reset password link has been sent, Please check your email to reset password.";

        /// <summary>
        /// The reset password link faild
        /// </summary>
        public static string ResetPasswordLinkFaild = "Either your is not active or not registered with us.";

        /// <summary>
        /// The operator service available in my area
        /// </summary>
        public static string OperatorServiceAvailableInMyArea = "Service available in your area";

        /// <summary>
        /// The operator service not available in my area
        /// </summary>
        public static string OperatorServiceNotAvailableInMyArea = "No Service available in your area, we will contact you soon.";

        /// <summary>
        /// The account created and verification link sent
        /// </summary>
        public static string AccountCreatedAndVerificationLinkSent = "Your account has been created successfully. Please check email and verify your account.";

        /// <summary>
        /// The operator account created
        /// </summary>
        public static string OperatorAccountCreated = "Your account has been created successfully. You can contact to admin to verify the account soon.";

        /// <summary>
        /// The image size exceed
        /// </summary>
        public static string ImageSizeExceed = "Please upload image upto 5 MB.";

        /// <summary>
        /// The address add success
        /// </summary>
        public static string AddressAddSuccess = "Address has been added successfully.";

        /// <summary>
        /// The address update success
        /// </summary>
        public static string AddressUpdateSuccess = "Address has been updated successfully.";

        /// <summary>
        /// The unauthorized message
        /// </summary>
        public static string UnauthorizedMessage = "You are not authorized to access api.";

        /// <summary>
        /// The oprator details u pdated
        /// </summary>
        public static string OpratorDetailsUPdated = "Operator details updated successfully.";

        /// <summary>
        /// The customer details u pdated
        /// </summary>
        public static string CustomerDetailsUPdated = "Customer details updated successfully.";

        /// <summary>
        /// The courier details u pdated
        /// </summary>
        public static string CourierDetailsUPdated = "Courier details updated successfully.";

        /// <summary>
        /// The system setting updated success
        /// </summary>
        public static string SystemSettingUpdatedSuccess = "System setting Updated successfully.";

        /// <summary>
        /// The ware house updae success
        /// </summary>
        public static string WareHouseUpdaeSuccess = "Warehouse updated successfully.";

        /// <summary>
        /// The ware house add success
        /// </summary>
        public static string WareHouseAddSuccess = "Warehouse added successfully.";

        /// <summary>
        /// The address not found
        /// </summary>
        public static string AddressNotFound = "Address not found or deleted.";

        /// <summary>
        /// The edit update failed
        /// </summary>
        public static string EditUpdateFailed = "Something went wrong,Please try agian later!";

        /// <summary>
        /// The address deleted or not found
        /// </summary>
        public static string AddressDeletedOrNotFound = "Address already deleted or not found!";

        /// <summary>
        /// The address deleted success
        /// </summary>
        public static string AddressDeletedSuccess = "Address deleted successfully.";

        /// <summary>
        /// The profile update success
        /// </summary>
        public static string ProfileUpdateSuccess = "Profile updated successfully.";

        /// <summary>
        /// The no item in cart
        /// </summary>
        public static string NoItemInCart = "Please add item in cart.";

        /// <summary>
        /// The sign out success
        /// </summary>
        public static string SignOutSuccess = "You have successfully logout.";

        /// <summary>
        /// The status success
        /// </summary>
        public static string StatusSuccess = "You have successfully changed status.";

        /// <summary>
        /// The shipment request updated success
        /// </summary>
        public static string ShipmentRequestUpdatedSuccess = "Shipment request Updated successfully.";

        /// <summary>
        /// The forgot password success
        /// </summary>
        public static string ForgotPasswordSuccess = "Forgot password request success.Please check your email to reset password.";

        /// <summary>
        /// The forgot password failed
        /// </summary>
        public static string ForgotPasswordFailed = "Either Email is not confirmed or user not registered with this email.";

        /// <summary>
        /// The profile updated successfully
        /// </summary>
        public static string ProfileUpdatedSuccessfully = "Profile updated successfully.";

        /// <summary>
        /// The failed to update pofile
        /// </summary>
        public static string FailedToUpdatePofile = "Failed to update profile.";

        /// <summary>
        /// The details not found
        /// </summary>
        public static string DetailsNotFound = "Details are not found.";

        /// <summary>
        /// The password change success
        /// </summary>
        public static string PasswordChangeSuccess = "Your password has been changed successfully.";

        /// <summary>
        /// The password change success
        /// </summary>
        public static string OldPasswordNewPasswordSame = "Old password and new password are same you can not set old password as new password.";

        /// <summary>
        /// The enquiry added successfully
        /// </summary>
        public static string EnquiryAddedSuccessfully = "Thank you, your query has been submitted successfully.";

        /// <summary>
        /// The trip type list fetched successfully
        /// </summary>
        public static string TripTypeListFetchedSuccessfully = "Trip types fetched successfully.";

        /// <summary>
        /// The fishing type list fetched successfully
        /// </summary>
        public static string FishingTypeListFetchedSuccessfully = "Fishing types fetched successfully.";

        /// <summary>
        /// The age grouped list fetched successfully
        /// </summary>
        public static string AgeGroupedListFetchedSuccessfully = "Age groups fetched successfully.";

        /// <summary>
        /// The no data found
        /// </summary>
        public static string NoDataFound = "No records were found.";

        /// <summary>
        /// The take fishing details submited successful
        /// </summary>
        public static string TakeFishingDetailsSubmitedSuccessful = "Your fishing details are submitted successfully.";

        /// <summary>
        /// The fail to submitdetails
        /// </summary>
        public static string FailToSubmitdetails = "Failed to submit details, please try again after some time.";

        /// <summary>
        /// The customer list fetched sucessfully
        /// </summary>
        public static string CustomerListFetchedSucessfully = "Customer list fetched sucessfully.";

        /// <summary>
        /// The trips are fetched successfully
        /// </summary>
        public static string TripsAreFetchedSuccessfully = "Trips details are fetched successfully.";

        /// <summary>
        /// The server error
        /// </summary>
        public static string ServerError = "The server encountered an unexpected condition.";

        /// <summary>
        /// The messages fetched succssfully
        /// </summary>
        public static string MessagesFetchedSuccssfully = "Messages fetched successfully.";

        /// <summary>
        /// The invalid in application receipt
        /// </summary>
        public static string InvalidInAppReceipt = "Invalid Receipt";

        /// <summary>
        /// The take fishing trip list fetched succssfully
        /// </summary>
        public static string TakeFishingTripListFetchedSuccssfully = "Take fishing trip list fetched successfully.";

        /// <summary>
        /// The go fishing details submited successful
        /// </summary>
        public static string GoFishingDetailsSubmitedSuccessful = "Your go fishing details are submitted successfully.";

        /// <summary>
        /// The join trip request submmited successfully
        /// </summary>
        public static string JoinTripRequestSubmmitedSuccessfully = "You've successfully sent the request to join the trip.";

        /// <summary>
        /// The join trip request failed
        /// </summary>
        public static string JoinTripRequestFailed = "Your request could not be sent, Please try again later. ";

        /// <summary>
        /// The trip number of pople is full
        /// </summary>
        public static string TheTripNumberOfPopleIsFull = "The Trip number of people is full.";

        /// <summary>
        /// The help out list successfully fetched
        /// </summary>
        public static string HelpOutListSuccessfullyFetched = "Help out list fetched successfully.";

        /// <summary>
        /// The log out sucessfully
        /// </summary>
        public static string LogOutSucessfully = "Log out successfully.";

        /// <summary>
        /// The failed to logout
        /// </summary>
        public static string FailedToLogout = "Failed to Log out.";

        /// <summary>
        /// The private users list for go fishing fetched sucessfully
        /// </summary>
        public static string PrivateUsersListForGoFishingFetchedSucessfully = "Private users list for go fishing fetched sucessfully.";

        /// <summary>
        /// The invalid login attempt
        /// </summary>
        public static string InvalidLoginAttempt = "Sorry , Email address is not associate with us.";

        public static string InvalidUser = "Sorry , This email address is not allow.";

        /// <summary>
        /// The user account in active contact admin
        /// </summary>
        public static string UserAccountInActiveContactAdmin = "Your account is inactive,please contact with your admin.";

        public static string YourAccountIsSuccessfullyCreated = "Your account is successfully created.";

        public static string TokenRefershed = "Token refresh successfully";

        public static string FailedToCreatePofile = "Faild to create profile";
    }
}
