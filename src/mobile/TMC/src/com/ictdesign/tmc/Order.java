/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package com.ictdesign.tmc;

/**
 * Defines the data structure of the order object as well as its functions.
 * 
 * Pretty self-explanatory.
 */

public class Order
{
	private String mOrderName = "";
	private String mOrderNumber = "";
	private String mOrderStatus = "";

	/**
	 * Constructor to initialize all the variables.
	 */

	public Order(String orderName, String orderNumber, String orderStatus)
	{
		mOrderName = orderName;
		mOrderNumber = orderNumber;
		mOrderStatus = orderStatus;
	}

	/**
	 * Set the order name.
	 */

	public void setOrderName(String orderName)
	{
		mOrderName = orderName;
	}

	/**
	 * Set the order number.
	 */

	public void setOrderNumber(String orderNumber)
	{
		mOrderNumber = orderNumber;
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

	public String getOrderName()
	{
		return mOrderName;
	}

	/**
	 * Get the order number.
	 */

	public String getOrderNumber()
	{
		return mOrderNumber;
	}

	/**
	 * Get the order status.
	 */

	public String getOrderStatus()
	{
		return mOrderStatus;
	}
}