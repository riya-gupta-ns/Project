using NS.CMS.Data.Entities;
using NS.CMS.Repository;
using NS.CMS.Model;

namespace NS.CMS.Buisness
{
  public class CandidateBuisness: ICandidateBuisness
  {
    private readonly ICandidateRepo _ICandidateRepo; 
    public CandidateBuisness(ICandidateRepo ICandidateRepo)
    {
      _ICandidateRepo = ICandidateRepo;
    }

    // Get All Candidates
    public List<Candidate> GetAllCandidates(){
      return _ICandidateRepo.GetAllCandidates();
    }

    public List<Candidate> GetCandidateByName(string candidateName){
      return _ICandidateRepo.GetCandidateByName(candidateName);
    }

    // Get Candidate By Id
    public List<Candidate> GetCandidateById(int id)
    {
      return _ICandidateRepo.GetCandidateById(id);
    }

    // Add A Candidate
    public bool AddCandidate(CandidateModel candidateModel, string wwwrootPath){
      return _ICandidateRepo.AddCandidate(candidateModel,wwwrootPath);
    }

    // Edit A Candidate
    public bool Edit(CandidateModel candidateModel,string wwwrootPath)
    {
      return _ICandidateRepo.Edit(candidateModel,wwwrootPath);
    }

    // Delete A Candidate
    public bool Delete(int id, CandidateModel candidateModel)
    {
      return _ICandidateRepo.Delete(id, candidateModel);
    }
  }
}
