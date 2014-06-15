package model;

public class Alarm
{

	private CharSequence id = null;
	private CharSequence type = null;
	private CharSequence description = null;
	private CharSequence time = null;

	public Alarm(String id, String type, String description, String time)
	{
		this.id = id;
		this.type = type;
		this.description = description;
		this.time = time;
	}

	public CharSequence getId()
	{
		return id;
	}

	public CharSequence getType()
	{
		return type;
	}

	public CharSequence getDescription()
	{
		return description;
	}

	public CharSequence getTime()
	{
		return time;
	}

}
