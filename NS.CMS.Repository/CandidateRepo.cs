using NS.CMS.Model; 
using NS.CMS.Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace NS.CMS.Repository
{
  public class CandidateRepo: ICandidateRepo
  {
   
    // Get All Candidates
    public List<Candidate>  GetAllCandidates()
    {
      using (var context = new CandidateDbContext() )
      {
         var res = context.Candidates.FromSqlRaw("UspSelectAllCandidates").ToList();
        //var res = context.Candidates.Where(x => x.Name.StartsWith(candidateName) || candidateName == null).ToList();
        return res;
      }
    }

    public List<Candidate>  GetCandidateByName(string candidateName)
    {
      using (var context = new CandidateDbContext() )
      {
         //var res = context.Candidates.FromSqlRaw("UspSelectAllCandidates").ToList();
        var res = context.Candidates.Where(x => x.Name.StartsWith(candidateName) || candidateName == null).ToList();
        return res;
      }
    }
   
    // Get Candidate By Id
    public List<Candidate> GetCandidateById(int id)
    {
      var context = new CandidateDbContext();
      var result = context.Candidates.FromSqlRaw("UspSelectCandidateById @Id", new SqlParameter("@Id", id));
      return result.ToList();
    }

    ////////////// Add a new Candidate ///////////
    public bool AddCandidate(CandidateModel candidateModel, string wwwrootPath)
    {
      // Resume Uploading
      string resumeFileName = Path.GetFileNameWithoutExtension(candidateModel.Resume.FileName);
      string resumeFileExtension = Path.GetExtension(candidateModel.Resume.FileName);

      string resumeName = resumeFileName + resumeFileExtension; 
      string resumePath = Path.Combine( wwwrootPath + "/resume", resumeName); 
      string resumePath1 = Path.Combine("/resume/",resumeName);

      candidateModel.Resume.CopyTo(new FileStream(resumePath,FileMode.Create));

      // Image Uploading
      string imageFileName = Path.GetFileNameWithoutExtension(candidateModel.Image.FileName);
      string imageFileExtension = Path.GetExtension(candidateModel.Image.FileName);

      string imageName = imageFileName + imageFileExtension;
      string imagePath = Path.Combine(wwwrootPath + "/image",imageName);
      string imagePath1 = Path.Combine("/image/", imageName);

      candidateModel.Image.CopyTo(new FileStream(imagePath,FileMode.Create));

      // Uploading Db
      using (var context = new CandidateDbContext())
      {
        var Name = new SqlParameter("@Name", candidateModel.Name);
        var Dob  = new SqlParameter("@Dob", candidateModel.Dob); 
        var Address = new SqlParameter("@Address", candidateModel.Address); 
        var Mobile  = new SqlParameter("@Mobile", candidateModel.Mobile); 
        var Email   = new SqlParameter("@Email", candidateModel.Email); 
        var Tech    = new SqlParameter("@Tech", candidateModel.Tech); 
        var Image   = new SqlParameter("@Image", imagePath1);
        var Resume  = new SqlParameter("@Resume", resumePath1);
        var Description  = new SqlParameter("@Description", candidateModel.Description);
        var Gender       = new SqlParameter("@Gender", candidateModel.Gender);
        
        context.Database.ExecuteSqlRaw("UspInsertIntoCandidates @Name, @Dob, @Address, @Mobile, @Email, @Tech, @Image, @Resume, @Description, @Gender", Name, Dob, Address, Mobile, Email, Tech, Image, Resume, Description, Gender);
      }
      return true;
    }
    /////////////////////////////////////////////////////////// /////////////////////////////////////////////////////////

    //////////////////// Edit A Candidate //////////////////////////////
    public bool Edit(CandidateModel candidateModel,string wwwrootPath)
    {
      string resumePath1 = "";
      string imagePath1 = "";

      if (candidateModel.Resume != null)
      {
        string resumeFileName = Path.GetFileNameWithoutExtension(candidateModel.Resume.FileName);
        string resumeFileExtension = Path.GetExtension(candidateModel.Resume.FileName);

        string resumeName = resumeFileName + resumeFileExtension;
        string resumePath = Path.Combine(wwwrootPath + "/resume", resumeName);
        resumePath1 = Path.Combine("/resume/", resumeName);

        candidateModel.Resume.CopyTo(new FileStream(resumePath, FileMode.Create));
      } else {
        var r = GetCandidateById(candidateModel.Id);
        resumePath1 = r.First().Resume;
      }

      // Image Uploading
      if(candidateModel.Image!=null)
      { 
        string imageFileName = Path.GetFileNameWithoutExtension(candidateModel.Image.FileName);
        string imageFileExtension = Path.GetExtension(candidateModel.Image.FileName);

        string imageName = imageFileName + imageFileExtension;
        string imagePath = Path.Combine(wwwrootPath + "/image", imageName);
        imagePath1 = Path.Combine("/image/", imageName);

        candidateModel.Image.CopyTo(new FileStream(imagePath, FileMode.Create));
      } else
      {
        var r = GetCandidateById(candidateModel.Id);
        imagePath1 = r.First().Image;
      }

      using (var context = new CandidateDbContext())
      {
        var paraamList = new List<SqlParameter>();
        paraamList.Add(new SqlParameter("@Id", candidateModel.Id));
        paraamList.Add(new SqlParameter("@Name", candidateModel.Name));
        paraamList.Add(new SqlParameter("@Dob", candidateModel.Dob));
        paraamList.Add(new SqlParameter("@Gender", candidateModel.Gender));
        paraamList.Add(new SqlParameter("@Address", candidateModel.Address));
        paraamList.Add(new SqlParameter("@Mobile", candidateModel.Mobile));
        paraamList.Add(new SqlParameter("@Email", candidateModel.Email));
        paraamList.Add(new SqlParameter("@Tech", candidateModel.Tech));
        paraamList.Add(new SqlParameter("@Describe", candidateModel.Description));
        paraamList.Add(new SqlParameter("@Image", imagePath1));
        paraamList.Add(new SqlParameter("@Resume", resumePath1));
        context.Database.ExecuteSqlRaw("UspUpdateCandidate @Id, @Name, @Dob, @Gender, @Address, @Mobile, @Email, @Tech, @Describe, @Image, @Resume", paraamList);
      }
      return true;
    }
    /////////////////////////////////////////////////////////// /////////////////////////////////////////////////////////

    // Delete a Candidate
    public bool Delete(int id, CandidateModel candidateModel)
    {
      using (var context = new CandidateDbContext())
      {
        var personWithId = context.Candidates.SingleOrDefault(x => x.Id == id);
        if(personWithId != null)
        {
          context.Remove(personWithId);
          context.SaveChanges();
          return true;
        }
        return false;
      }
    }
  }
}
