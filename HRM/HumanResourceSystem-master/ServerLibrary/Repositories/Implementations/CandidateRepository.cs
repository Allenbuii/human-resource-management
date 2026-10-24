
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using System.Net;
using System.Net.Mail;

namespace ServerLibrary.Repositories.Implementations
{
    public class CandidateRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Candidate>, ICandidateInterface
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var dep = await appDbContext.Candidates.FindAsync(id);
            if (dep is null) return NotFound();

            appDbContext.Candidates.Remove(dep);
            await Commit();
            return Success();
        }

        public async Task<List<Candidate>> GetAll() => await appDbContext
            .Candidates
            .AsNoTracking()
            .Include(d => d.Recruitment)
            .ToListAsync();
        public async Task<Candidate> GetById(int id) => await appDbContext.Candidates.FindAsync(id);

        public async Task<GeneralResponse> Insert(Candidate item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, $"{item.Name} already added");
            appDbContext.Candidates.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Candidate item)
        {
            var candidate = await appDbContext.Candidates.FindAsync(item.Id);
            if (candidate is null) return NotFound();
            candidate.Name = item.Name;
            candidate.Email = item.Email;
            candidate.Address = item.Address;
            candidate.TelephoneNumber = item.TelephoneNumber;
            candidate.ApplicationDate = item.ApplicationDate;
            candidate.Status = item.Status;
            candidate.CVLink = item.CVLink;
            candidate.RecruitmentId = item.RecruitmentId;
            candidate.Recruitment = item.Recruitment;
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> SendEmail(Candidate item)
        {
            string fromMail = "yuukihasoma@gmail.com";
            string formPassword = "qtsvnidqmhxuhksj";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.To.Add(new MailAddress(item.Email));

            if (item.Status == "Interview")
            {
                message.Subject = $"Invitation to Interview for {item.Recruitment.JobName} Position";
                message.Body = $@"
                <html>
                    <body>
                        <p>Dear {item.Name},</p>
                        <p>We are pleased to invite you for an interview for the {item.Recruitment.JobName} position.</p>
                        <p>Please let us know your availability for the interview.</p>
                        <p>Best regards,</p>
                    </body>
                </html>";
            }
            if (item.Status == "Accepted")
            {
                message.Subject = $"Job Offer for {item.Recruitment.JobName} Position";
                message.Body = $@"
                <html>
                    <body>
                        <p>Dear {item.Name},</p>
                        <p>Congratulations! We are excited to offer you the {item.Recruitment.JobName} position at our company.</p>
                        <p>Please find attached the offer letter and further instructions.</p>
                        <p>Best regards,</p>
                    </body>
                </html>";
            }
            if (item.Status == "Rejected")
            {
                message.Subject = $"Application Status for {item.Recruitment.JobName} Position";
                message.Body = $@"
                <html>
                    <body>
                        <p>Dear {item.Name},</p>
                        <p>Thank you for applying for the {item.Recruitment.JobName} position.</p>
                        <p>We regret to inform you that we have decided to move forward with other candidates at this time.</p>
                        <p>We wish you all the best in your job search and future endeavors.</p>
                        <p>Best regards,</p>
                    </body>
                </html>";
            }

            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, formPassword),
                EnableSsl = true,
            };

            await smtpClient.SendMailAsync(message);
            return Success();
        }

        private async Task Commit() => await appDbContext.SaveChangesAsync();
        private static GeneralResponse NotFound() => new(false, "Sorry candidate not found");
        private static GeneralResponse Success() => new(true, "Process completed");
        private async Task<bool> CheckName(string name)
        {
            var item = await appDbContext.Candidates.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}
