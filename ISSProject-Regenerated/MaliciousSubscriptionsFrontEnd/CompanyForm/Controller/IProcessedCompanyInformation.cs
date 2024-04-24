namespace ISSProject.CompanyForm.Controller
{
    internal interface IProcessedCompanyInformation
    {
        bool CommitTokenToDatabase();
        bool ValidateCompanyToken();
    }
}