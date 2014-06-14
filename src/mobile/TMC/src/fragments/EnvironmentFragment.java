/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package fragments;

import org.json.JSONObject;
import model.Constants;
import ictd.activities.R;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import android.widget.Toast;

public class EnvironmentFragment extends Fragment
{

	private ResultReceiver receiver;
	private TextView dustView;
	private TextView humidityView;
	private TextView lightView;
	private TextView soundView;
	private TextView temperatureView;
	private Timer timer;

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState)
	{
		View rootView = inflater.inflate(R.layout.fragment_environment,
				container, false);

		dustView = (TextView) rootView.findViewById(R.id.environment_dust_tv);

		humidityView = (TextView) rootView
				.findViewById(R.id.environment_humidity_tv);

		lightView = (TextView) rootView.findViewById(R.id.environment_light_tv);

		soundView = (TextView) rootView.findViewById(R.id.environment_sound_tv);

		temperatureView = (TextView) rootView
				.findViewById(R.id.environment_temperature_tv);

		return rootView;
	}

	private void makeService(int envUpdateCommand)
	{
		Intent service = new Intent(getActivity(),
				services.EnvUpdateService.class);
		Bundle parcel = new Bundle();
		parcel.putInt("command", Constants.ENV_UPDATE_COMMAND);
		service.putExtra("parcel", parcel);
		getActivity().stopService(service);
		getActivity().startService(service);
	}

	public void onStart()
	{
		super.onStart();

		receiver = new ResultReceiver();
		getActivity()
				.registerReceiver(
						receiver,
						new IntentFilter(Integer
								.toString(Constants.ENV_UPDATE_COMMAND)));
		makeService(Constants.ENV_UPDATE_COMMAND);
		startTimer(Constants.UPDATE_INTERVAL);
	}

	public void onStop()
	{
		super.onStop();
		getActivity().unregisterReceiver(receiver);
		timer.cancel(true);
	}

	private class ResultReceiver extends BroadcastReceiver
	{

		@Override
		public void onReceive(Context context, Intent intent)
		{
			handleEnvUpdate(intent.getStringExtra("result"));
		}
	}

	private void handleEnvUpdate(String response)
	{
		try
		{
			JSONObject obj = new JSONObject(response);
			String result = obj.getString(Constants.RESULT);

			if (result.equalsIgnoreCase("success"))
			{
				dustView.setText(obj.getString("Dust") + "pcs/liter");
				humidityView.setText(obj.getString("Humidity") + "%");
				lightView.setText(obj.getString("Light") + "cd");
				soundView.setText(obj.getString("Sound") + "dB");
				temperatureView.setText(obj.getString("Temperature")
						+ (char) 0x00B0 + "C");
				Toast.makeText(getActivity(), "Updated environment data",
						Toast.LENGTH_SHORT).show();// to be removed
			}
			else
				Toast.makeText(getActivity(), Constants.ENV_UPDATE_FAIL,
						Toast.LENGTH_SHORT).show();
		}
		catch (Exception exc)
		{
			Toast.makeText(getActivity(), Constants.ENV_UPDATE_FAIL,
					Toast.LENGTH_SHORT).show();
		}
	}

	private void startTimer(long i)
	{
		timer = new Timer();
		timer.execute(i);
	}

	private class Timer extends AsyncTask<Long, Object, Object>
	{
		@Override
		protected String doInBackground(Long... params)
		{
			try
			{
				Thread.sleep(params[0]);
				return "done";
			}

			catch (InterruptedException e)
			{
				e.printStackTrace();
				return "done";
			}
		}

		@Override
		protected void onPostExecute(Object result)
		{
			// this creates regular updates of the view
			makeService(Constants.ENV_UPDATE_COMMAND);
			startTimer(Constants.UPDATE_INTERVAL);
		}

	}

}
