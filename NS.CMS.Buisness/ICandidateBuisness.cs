using NS.CMS.Data.Entities;
using NS.CMS.Model;

namespace NS.CMS.Buisness
{
  public interface ICandidateBuisness
  {
    public List<Candidate> GetAllCandidates();
    public List<Candidate> GetCandidateById(int id);
    public List<Candidate> GetCandidateByName(string candidateName);

    public bool AddCandidate(CandidateModel candidateModel, string wwwrootPath);

    public bool Edit(CandidateModel candidateModel, string wwwrootPath);

    public bool Delete(int id, CandidateModel candidateModel);
  }
}
