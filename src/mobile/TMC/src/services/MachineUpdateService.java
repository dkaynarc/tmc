package services;



import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import model.Constants;
import android.app.IntentService;
import android.content.Intent;
import android.os.Bundle;


public class MachineUpdateService extends IntentService 
{
    private String urlString = Constants.SERVER_URL;
    //private String urlString = "http://172.19.14.150:9000/api/Server/";    
    private int command;

 
     
     public MachineUpdateService()
     {
		super("SynchService");
	 }
    

     
	@Override
	protected void onHandleIntent(Intent intent) 
	{
	   Bundle parcel = intent.getBundleExtra("parcel");
	   command =	parcel.getInt("command");
   	   urlString +=  "GetMachineryStatus" ; 
	    
	   String response =  connect(urlString);	
	    
	   notifyCaller(response);
	}









    private String connect(String urlStr) 
	{
    	StringBuilder str = new StringBuilder();
     
    	
	 try{ 
		  URL url = new URL(urlStr);
		  HttpURLConnection con = (HttpURLConnection) url.openConnection();
		  con.setRequestMethod("GET");
		  con.setConnectTimeout(10000); // times out after 10 seconds
		  con.setRequestProperty("Content-Type","application/json");
		  con.setRequestProperty("Accept", "application/json");
	      BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));
   

		  String line;
		 
	      while((line = in.readLine()) !=null )
		  {
		    str.append(line);
		  }	
		  in.close();
	    }
	 
	 
	 catch(Exception exc)
	 {
	   System.out.print("Exception: " + exc.toString());
	 }	
     
     return str.toString();
    	
		 
}









private void notifyCaller(String response) 
{
	Intent intent = new Intent();	
	intent.setAction(Integer.toString(command));
	intent.putExtra("command", Integer.toString(command));
	intent.putExtra("result", response);	   
	sendBroadcast(intent);
}

}


