using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TestApp.Core.Interface.Repository;
using TestApp.Web.Models.Authorization.Requirement.Operation;
using System.Threading.Tasks;

namespace TestApp.Web.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IDocumentRepository _documentRepository;

        public DocumentController(IAuthorizationService authorizationService,
                                  IDocumentRepository documentRepository)
        {
            _authorizationService = authorizationService;
            _documentRepository = documentRepository;
        }

        public async Task<IActionResult> Get(int documentId)
        {
            var doc = _documentRepository.Get(documentId);

            if (doc == null)
            {
                return new NotFoundResult();
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, doc, CrudOperationRequirement.Read);

            if (authorizationResult.Succeeded)
            {
                return Ok();
            }
            else if (User.Identity.IsAuthenticated)
            {
                return new ForbidResult();
            }
            else
            {
                //when authentication must be performed
                return new ChallengeResult();
                //may be appropriate to redirect the user to a login page
            }
        }
    }
}