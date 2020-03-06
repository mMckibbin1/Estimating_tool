using Estimating_Tool.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Estimating_Tool.DAL
{
	public class Parser
	{
		Estimatingcontext context = new Estimatingcontext();

		public int CheckForAssignedManagers()
		{
			int count = 0;
			count = count + context.Consultants.Where(x => x.ManagerId == null).ToList().Count();
			count = count + context.Teams.Where(x => x.ManagerId == null).ToList().Count();
			return count;
		}

		//this sets a session variable to true if the person logged in is listed as a manager in the Manager database table. 
		//the admin section of the nav bar is only visible if this is set to true
		public bool Security(string userName)
		{
			List<Manager> managers = context.Managers.ToList();
			if (managers.Any(x => x.Username.ToLower() == userName.ToLower()))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		//This method compares the Consultant names in an uploaded file with the Consultants listed in the consultant database
		// and Creates a new consultant for every missing name.
		public string ConvertFile(string filepath)
		{
			string errorMessage = "";
			using (var reader = new StreamReader(filepath))
			{
				List<string> namesFromFile = new List<string>();
				List<string> teamNamesFromFile = new List<string>();

				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					var values = line.Split(',');
					if (values.Count() != 30)
					{
						errorMessage = "Invalid file - incorrect number of columns!";
						return errorMessage;
					}
					//ignores header row
					if (values[0] == "WeekNo")
					{
						continue;
					}
					//stops when an empty row is hit
					if (values[0] == "")
					{
						break;
					}
					//adds the name from the "LastAnalyst" column in the CSV
					namesFromFile.Add(values[22]);
					teamNamesFromFile.Add(values[18]);
				}
				AddMissingTeamsAndConsultants(namesFromFile, teamNamesFromFile);
				//RefactorFileAndAdd(filepath);
			}

			File.Delete(ConfigurationManager.AppSettings["uploadedCSV"]);
			return errorMessage;
		}

		//takes the list of incoming consultant and team names and creates records for them if they are missing 
		public void AddMissingTeamsAndConsultants(List<string> incomingConsultantNames, List<string> incomingTeamNames)
		{
			List<string> namesFromDatabase = new List<string>();
			List<string> ConsultantsNotInDB = new List<string>();

			// Builds a list of names from the consultant table
			List<Consultant> consultants = context.Consultants.ToList();
			foreach (Consultant con in consultants)
			{
				namesFromDatabase.Add(con.Firstname + " " + con.Lastname);
			}

			//Checks lastAnalyst from IPCs being added against Consultants in the application database and creates a consultant if there is no match 
			ConsultantsNotInDB.AddRange(incomingConsultantNames.Where(x => !namesFromDatabase.Any(y => y.ToLower() == x.ToLower())).Select(z => z).Distinct().ToList());
			if (ConsultantsNotInDB.Count > 0)
			{
				foreach (string name in ConsultantsNotInDB)
				{
					string[] names = name.Split(' ');
					context.Consultants.Add(new Consultant
					{
						Username = "v1\\" + names[1] + names[0][0],
						Firstname = names[0],
						Lastname = name.Remove(0, (names[0].Length + 1))
					});
				}
				context.SaveChanges();
			}
			//gets the names of the team rather than the team ID
			//checks to see if a team exists and adds them if not
			List<string> TeamsNotInDB = new List<string>();
			TeamsNotInDB.AddRange(incomingTeamNames.Where(x => !context.Teams.Any(y => y.TeamName.ToLower() == x.ToLower())).Select(z => z).Distinct().ToList());
			if (TeamsNotInDB.Count > 0)
			{
				foreach (string name in TeamsNotInDB)
				{
					context.Teams.Add(new Team { TeamName = name });
				}
				context.SaveChanges();
			}
		}

		//converts string lists to IPC's


		//public IPC RowConvert(DataRow Row)
		//{
		//	IPC row = new IPC();
		//	double j = 0.0;
		//	int result;

		//	int.TryParse(Row["MonthNo"].ToString(), out result);
		//	row.MonthNo = result;
		//	row.PMGuid = (Guid)Row["IPCGUID"];
		//	row.IPCNumber = (int.Parse(Row["IPC"].ToString()));
		//	row.Customer = Row["Customer"].ToString();
		//	row.Project = Row["Project"].ToString();
		//	row.Title = Row["Title"].ToString();
		//	row.Ipctype = Row["Type"].ToString();
		//	row.Source = Row["Source"].ToString();
		//	row.InitialResponse = Row["InitialResponse"].ToString();
		//	row.ReassignmentNotes = Row["ReAssignment"].ToString();
		//	row.ConfigItem = Row["ConfigItem"].ToString();
		//	row.ResolutionDescription = Row["Resolution"].ToString();
		//	row.ResolutionCategory = Row["ResolutionCategory"].ToString();
		//	row.Chargeable = Row["Chargeable"].ToString();
		//	row.EstEffort = (double.TryParse(Row["EstEffort"].ToString(), out j) ? j : (double?)null);
		//	row.EstefforttoDate = (double.TryParse(Row["ActualEffort"].ToString(), out j) ? j : (double?)null);
		//	row.Breached = Row["Breached"].ToString();
		//	row.AnalystGroup = Row["AssignedTeam"].ToString();
		//	row.LastAnalyst = Row["LAstAnalyst"].ToString();
		//	row.Po = Row["PO"].ToString();
		//	row.OverRun = Row["OverRun"].ToString();
		//	row.Audit = Row["Audit"].ToString() == "1" ? true : false;
		//	row.AuditSelection = Row["AuditSelection"].ToString();

		//	try
		//	{
		//		row.Updated = (DateTime)Row["UpdatedDate"];
		//	}
		//	catch
		//	{
		//		row.Updated = DateTime.Now;
		//	}
		//	row.ExtractedDate = DateTime.Now;
		//	//row.LastNote = Row[28];
		//	//row.CustomerReference = Row[29];
		//	return row;
		//}

		//public IPC OldRowConvert(List<string> Row)
		//{
		//	IPC row = new IPC();
		//	double j = 0.0;
		//	row.MonthNo = (Int32.TryParse(Row[0], out int i) ? i : (int?)null);
		//	row.PMGuid = Guid.Parse(Row[1]);
		//	row.Ipctype = Row[2];
		//	row.IPCNumber = (int.Parse(Row[3]));
		//	row.InitialResponse = Row[4];
		//	row.PacmanCode = Row[5];
		//	row.Customer = Row[6];
		//	row.Project = Row[7];
		//	row.Title = Row[8];
		//	row.Description = Row[9];
		//	row.Status = Row[10];
		//	//note returned data values 11 and 12 relate to impact and urgency rating in DB, request was to just have priority 
		//	//represent both of these so urgency was chosen
		//	row.Priority = Row[12];
		//	row.Chargeable = Row[13];
		//	row.EstEffort = (double.TryParse(Row[14], out j) ? j : (double?)null);
		//	row.EstefforttoDate = (double.TryParse(Row[15], out j) ? j : (double?)null);
		//	row.Breached = Row[16];
		//	row.Itype = Row[17];
		//	row.AnalystGroup = Row[18];
		//	row.ConsultantId = (Int32.TryParse(Row[19], out i) ? i : (int?)null);
		//	row.ResolutionDescription = Row[20];
		//	//added because there is currently an issue with the date formatting from the query used to pull
		//	//the records from the Landesk database. until that issue is fixed the try will always fail
		//	//meaning the updated field will always be the date the record was added to the IPC quality checker
		//	try
		//	{
		//		row.Updated = Convert.ToDateTime(Row[21]);
		//	}
		//	catch
		//	{
		//		row.Updated = DateTime.Now;
		//	}
		//	row.LastAnalyst = Row[22];
		//	row.Po = Row[23];
		//	row.OverRun = Row[24];
		//	row.Source = Row[25];
		//	row.ConfigItem = Row[26];
		//	row.ResolutionCategory = Row[27];
		//	row.ExtractedDate = DateTime.Now;
		//	row.LastNote = Row[28];
		//	row.CustomerReference = Row[29];
		//	return row;
		//}

		//used to read the uploaded file and convert each row into IPC objects which are then added to the database context
		//public void RefactorFileAndAdd(string filepath)
		//{
		//	List<> rows = new List<IPC>();
		//	using (var reader = new StreamReader(filepath))
		//	{
		//		while (!reader.EndOfStream)
		//		{
		//			//removing instances of "<" and ">" from input data and replacing with " as they can throw a security 
		//			//exception when calling the HTTPPost Edit function in the controler
		//			var dirtyLine = reader.ReadLine();
		//			var cleanLine = dirtyLine.Replace('<', '\"').Replace('>', '\"');
		//			List<string> tempLine = cleanLine.Split(',').ToList();
		//			if (tempLine[0] == "WeekNo")
		//			{
		//				continue;
		//			}
		//			if (tempLine[0] == "")
		//			{
		//				break;
		//			}
		//			rows.Add(OldRowConvert(tempLine));
		//		}
		//		FixTablesAndAdd(rows);
		//	}
		//}
		//public void FixTablesAndAdd(List<IPC> rows)
		//{
		//	List<IPC> toAdd = new List<IPC>();
		//	List<Consultant> consultants = context.Consultants.ToList();
		//	List<IPC> tempIPCList = new List<IPC>();

		//	//assign the correct consultantId and ManagerId to each IPC
		//	foreach (IPC row in rows)
		//	{
		//		foreach (Consultant con in consultants)
		//		{
		//			if (row.LastAnalyst.ToLower() == (con.Firstname.ToLower() + " " + con.Lastname.ToLower()))
		//			{
		//				row.ConsultantId = con.Id;
		//				row.ManagerId = con.ManagerId;
		//				break;
		//			}
		//		}
		//	}

		//	//creates sub lists of IPC's by Consultants in the database. takes 10 random IPC's per Consultant and adds them to the
		//	// toAdd list, which is then added to the IPC table in the database
		//	toAdd.Clear();
		//	foreach (Consultant consultant in consultants)
		//	{
		//		Random rnd = new Random();
		//		tempIPCList.Clear();

		//		tempIPCList = rows.Where(x => x.ConsultantId == consultant.Id).ToList();
		//		if (tempIPCList.Count <= 10)
		//		{
		//			toAdd.AddRange(tempIPCList);
		//		}
		//		else
		//		{

		//			toAdd.AddRange(tempIPCList.OrderBy(x => rnd.Next()).Take(10));
		//		}
		//	}
		//	context.IPCs.AddRange(toAdd);
		//	context.SaveChanges();
		//}
	}
}
