
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using System.Net.Mail;
using System.Net;

namespace ServerLibrary.Repositories.Implementations
{
    public class PeriodicEvaluationRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<PeriodicEvaluation>, IPeriodicEvaluationInterface
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.PeriodicEvaluations.FindAsync(id);
            if (item is null) return NotFound();

            appDbContext.PeriodicEvaluations.Remove(item);
            await Commit();
            return Success();
        }

        public async Task<List<PeriodicEvaluation>> GetAll()
        {
            var instructors = await appDbContext.PeriodicEvaluations
                .AsNoTracking()
                .Include(e => e.Employee)
                .ToListAsync();

            return instructors;
        }

        public async Task<List<PeriodicEvaluation>> GetAllByEmployeeId(int employeeid)
        {
            var instructors = await appDbContext.PeriodicEvaluations
                .AsNoTracking()
                .Include(e => e.Employee)
                .Where(p => p.EmployeeId == employeeid)
                .ToListAsync();

            return instructors;
        }

        public async Task<PeriodicEvaluation> GetClosestByEmployeeId(int employeeid)
        {
            var evaluation = await appDbContext.PeriodicEvaluations
                .AsNoTracking()
                .Include(e => e.Employee)
                .OrderByDescending(e => e.Id)
                .FirstOrDefaultAsync(pe => pe.EmployeeId == employeeid);

            if (evaluation == null)
            {
                return new PeriodicEvaluation();
            }

            return evaluation!;
        }

        public async Task<PeriodicEvaluation> GetById(int id) => await appDbContext.PeriodicEvaluations.FindAsync(id);

        public async Task<GeneralResponse> Insert(PeriodicEvaluation item)
        {
            appDbContext.PeriodicEvaluations.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(PeriodicEvaluation item)
        {
            var periodicEvaluations = await appDbContext.PeriodicEvaluations.FirstOrDefaultAsync(i => i.Id == item.Id);
            if (periodicEvaluations is null) return new GeneralResponse(false, "Instructor does not exist");

            periodicEvaluations.Name = item.Name;
            periodicEvaluations.EvaluationDate = item.EvaluationDate;
            periodicEvaluations.TechnicalSkillsScore = item.TechnicalSkillsScore;
            periodicEvaluations.CommunicationSkillsScore = item.CommunicationSkillsScore;
            periodicEvaluations.TeamworkSkillsScore = item.TeamworkSkillsScore;
            periodicEvaluations.ProblemSolvingSkillsScore = item.ProblemSolvingSkillsScore;
            periodicEvaluations.WorkEthicScore = item.WorkEthicScore;
            periodicEvaluations.Comments = item.Comments;
            periodicEvaluations.EmployeeId = item.EmployeeId;
            periodicEvaluations.Employee = item.Employee;

            await appDbContext.SaveChangesAsync();
            await Commit();
            return Success();
        }
        public async Task<GeneralResponse> SendEmail(string messageType, PeriodicEvaluation item)
        {
            string fromMail = "yuukihasoma@gmail.com";
            string formPassword = "qtsvnidqmhxuhksj";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.To.Add(new MailAddress(item.Employee.Email));

            string[] monthNames = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            string monthName = monthNames[item.EvaluationDate.Month - 1];

            if (messageType == "Add")
            {
                message.Subject = $"Monthly Performance Evaluation Scores - {monthName}";
                message.Body = $@"
                <html>
                    <body>
                        <p>Dear {item.Employee.Name},</p>

                        <p>I hope this message finds you well. As part of our ongoing commitment to your professional development and growth, we have completed the performance evaluations for this month. Below, you will find your scores in various competency areas:</p>

                        <ul>
                            <li><strong>Technical Skills Score</strong>: {item.TeamworkSkillsScore}</li>
                            <li><strong>Communication Skills Score</strong>: {item.CommunicationSkillsScore}</li>
                            <li><strong>Teamwork Skills Score</strong>: {item.TeamworkSkillsScore}</li>
                            <li><strong>Problem Solving Skills Score</strong>: {item.ProblemSolvingSkillsScore}</li>
                            <li><strong>Work Ethic Score</strong>: {item.WorkEthicScore}</li>
                        </ul>

                        <p>Comment: {item.Comments}</p>

                        <p>These scores are based on your performance over the past month. We encourage you to review your scores and reflect on areas where you can continue to excel and areas that may benefit from further development.</p>

                        <p>If you have any questions or would like to discuss your evaluation in more detail, please feel free to schedule a meeting with me.</p>

                        <p>Thank you for your hard work and dedication.</p>

                        <p>Best regards,</p>
                    </body>
                </html>";
            }
            if (messageType == "Update")
            {
                message.Subject = $"Update: Performance Evaluation Scores - {monthName}";
                message.Body = $@"
                <html>
                    <body>
                        <p>Dear {item.Employee.Name},</p>

                        <p>This email is to inform you that your performance evaluation scores have been updated.</p>

                        <ul>
                            <li><strong>Technical Skills Score</strong>: {item.TechnicalSkillsScore}</li>
                            <li><strong>Communication Skills Score</strong>: {item.CommunicationSkillsScore}</li>
                            <li><strong>Teamwork Skills Score</strong>: {item.TeamworkSkillsScore}</li>
                            <li><strong>Problem Solving Skills Score</strong>: {item.ProblemSolvingSkillsScore}</li>
                            <li><strong>Work Ethic Score</strong>: {item.WorkEthicScore}</li>
                        </ul>

                        <p>Comment: {item.Comments}</p>

                        <p>Please review your updated scores. If you have any questions or need further clarification, please feel free to contact me.</p>

                        <p>Thank you for your attention to this matter.</p>

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
        private static GeneralResponse NotFound() => new(false, "Sorry instructor not found");
        private static GeneralResponse Success() => new(true, "Process completed");
    }
}
