/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package com.ictdesign.tmc;

/**
 * Defines the data structure of the machine object as well as its functions.
 * 
 * Pretty self-explanatory.
 */

public class Machine
{
	private String mMachineName = "";
	private String mMachineType = "";
	private String mMachineStatus = "";

	/**
	 * Constructor to initialize all the variables.
	 */

	public Machine(String machineName, String machineType, String machineStatus)
	{
		mMachineName = machineName;
		mMachineType = machineType;
		mMachineStatus = machineStatus;
	}

	/**
	 * Set the machine name.
	 */

	public void setMachineName(String machineName)
	{
		mMachineName = machineName;
	}

	/**
	 * Set the machine number.
	 */

	public void setMachineType(String machineType)
	{
		mMachineType = machineType;
	}

	/**
	 * Set the machine type.
	 */

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
	 * Get the machine type.
	 */

	public String getMachineType()
	{
		return mMachineType;
	}

	/**
	 * Get the machine status.
	 */

	public String getMachineStatus()
	{
		return mMachineStatus;
	}
}