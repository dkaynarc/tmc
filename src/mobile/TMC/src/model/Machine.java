/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package model;

/**
 * Defines the data structure of the machine object as well as its functions.
 * 
 * Pretty self-explanatory.
 */

public class Machine
{
	private String mMachineName = "";
	private String mMachineStatus = "";

	/**
	 * Constructor to initialize all the variables.
	 */

	public Machine(String machineName, String machineStatus)
	{
		mMachineName = machineName;
		mMachineStatus = machineStatus;
	}

	/**
	 * Set the machine name.
	 */

	public void setMachineName(String machineName)
	{
		mMachineName = machineName;
	}

	public void setMachineStatus(String machineStatus)
	{
		mMachineStatus = machineStatus;
	}

	/**
	 * Get the machine name.
	 */

	public String getMachineName()
	{
		return mMachineName;
	}

	/**
	 * Get the machine status.
	 */

	public String getMachineStatus()
	{
		return mMachineStatus;
	}
}