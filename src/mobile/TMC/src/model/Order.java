/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package model;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;

/**
 * Defines the data structure of the order object as well as its functions.
 * 
 * Pretty self-explanatory.
 */

public class Order
{
	private int mOrderId = 0;
	private String mOrderOwner = "";
	private String mOrderStatus = "";
	private int mBlack = 0;
	private int mBlue = 0;
	private int mRed = 0;
	private int mGreen = 0;
	private int mWhite = 0;
	private String mStartTime = null;
	private String mFinishTime = null;

	/**
	 * Constructor to initialize all the variables.
	 */

	public Order(int orderId, String orderOwner, String orderStatus, int black,
			int blue, int green, int red, int white)
	{
		mOrderId = orderId;
		mOrderOwner = orderOwner;
		mOrderStatus = orderStatus;
		mBlack = black;
		mBlue = blue;
		mGreen = green;
		mRed = red;
		mWhite = white;
	}

	// //////////////////////////////////////////
	public Order(String orderOwner, int black, int blue, int green, int red,
			int white)
	{
		mOrderOwner = orderOwner;
		mBlack = black;
		mBlue = blue;
		mGreen = green;
		mRed = red;
		mWhite = white;
	}

	// ///////////////////////////////////////////////////////////////////



	public void setColourNumber(String colourName, int quantity)
	{
		if (colourName.equalsIgnoreCase("black"))
			mBlack = quantity;
		if (colourName.equalsIgnoreCase("blue"))
			mBlue = quantity;
		if (colourName.equalsIgnoreCase("green"))
			mGreen = quantity;
		if (colourName.equalsIgnoreCase("red"))
			mRed = quantity;
		if (colourName.equalsIgnoreCase("white"))
			mWhite = quantity;
	}

	public int getColourNumber(String colourName)
	{
		if (colourName.equalsIgnoreCase("black"))
			return mBlack;
		if (colourName.equalsIgnoreCase("blue"))
			return mBlue;
		if (colourName.equalsIgnoreCase("green"))
			return mGreen;
		if (colourName.equalsIgnoreCase("red"))
			return mRed;
		if (colourName.equalsIgnoreCase("white"))
			return mWhite;
		return 0;
	}

	/**
	 * Set the order name.
	 */

	public void setOrderOwner(String orderOwner)
	{
		mOrderOwner = orderOwner;
	}

	/**
	 * Set the order number.
	 */

	public void setOrderId(int orderId)
	{
		mOrderId = orderId;
	}

	/**
	 * Set the order type.
	 */

	public void setOrderStatus(String orderStatus)
	{
		mOrderStatus = orderStatus;
	}

	/**
	 * Get the order name.
	 */

	public String getOrderOwner()
	{
		return mOrderOwner;
	}

	/**
	 * Get the order number.
	 */

	public int getOrderId()
	{
		return mOrderId;
	}

	/**
	 * Get the order status.
	 */

	public String getOrderStatus()
	{
		return mOrderStatus;
	}

	public int getQuantity()
	{
		int quantity = mBlack + mBlue + mGreen + mRed + mWhite;
		return quantity;
	}

	public String getStartTime()
	{
		if (mStartTime == null)
			return "--/--/--- --:--:--";
	
		try
		{
	       return (new SimpleDateFormat("dd/MM/yyyy HH:mm:ss", Locale.getDefault()))
				.format(mStartTime);
		}
		catch(Exception exc)
		{
			return mStartTime;
		}
	}

	public String  getFinishTime()
	{
		if (mFinishTime == null)
			return "--/--/--- --:--:--";
		
		try
		{
	       return (new SimpleDateFormat("dd/MM/yyyy HH:mm:ss", Locale.getDefault()))
				.format(mFinishTime);
		}
		catch(Exception exc)
		{
			return mFinishTime;
		}
	}
	
	
	
	public void setStartTime(String startTime)
	{
	    mStartTime = startTime;
	}

	public void setFinishTime(String finishTime)
	{
        mFinishTime = finishTime;
	}
}