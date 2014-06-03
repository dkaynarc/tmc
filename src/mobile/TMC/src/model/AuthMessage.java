package model;

public class AuthMessage
{

	private String Result;
	private String Name;
	private String Role;
	
	public String getResult()
	{
		return Result;
	}

	public String getUserName()
	{
		return Name;
	}

	public void setResult(String result)
	{
		Result = result;
	}

	public void setUserName(String userName)
	{
		Name = userName;
	}

	public String getRoleName() {
	
		return Role;
	}
	
	public void setRoleName(String role) {
		
		Role = role;
	}

}
