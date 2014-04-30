/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package model;

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
	private int numberOfItems;
    private int black;
    private int blue;
    private int red;
    private int green;
    private int white;
   	
	/**
	 * Constructor to initialize all the variables.
	 */
    
    
   
   
	public Order(String orderName, String orderNumber, String orderStatus)
	{
		mOrderName = orderName;
		mOrderNumber = orderNumber;
		mOrderStatus = orderStatus;
	}

	
	////////////////////////////////////////////
	public Order(String mOrderName, int numberOfItems) 
	{
		this.mOrderName = mOrderName;
		this.numberOfItems = numberOfItems;
	}
	
	
	 public void setColourNumber(String colourName, int quantity )
	    {
	    	if(colourName.equalsIgnoreCase("black"))black = quantity;
	    	if(colourName.equalsIgnoreCase("blue"))blue = quantity;
	    	if(colourName.equalsIgnoreCase("red"))red = quantity;
	    	if(colourName.equalsIgnoreCase("white"))white = quantity;
	    	if(colourName.equalsIgnoreCase("green"))green = quantity; 	
	    }

	   
	   public int getColourNumber(String colourName)
	   {
	   	 if(colourName.equalsIgnoreCase("black"))return black;
	   	 if(colourName.equalsIgnoreCase("blue")) return blue;
	   	 if(colourName.equalsIgnoreCase("red"))return red;
	   	 if(colourName.equalsIgnoreCase("white"))return white;
	   	 if(colourName.equalsIgnoreCase("green"))return green;
		 return 0; 	
	   }
	////////////////////////////////////////////

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

	public int getNumberOfItems() {
	
		return numberOfItems;
	}
}