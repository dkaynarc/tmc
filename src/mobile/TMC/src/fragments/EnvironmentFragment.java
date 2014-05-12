/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package fragments;

import ictd.activities.R;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;



public class EnvironmentFragment extends Fragment
{

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState)
	{
		View rootView = inflater.inflate(R.layout.fragment_environment,
				container, false);

		String dust = "";
		// Set dust to dust value
		((TextView) rootView.findViewById(R.id.environment_dust_tv))
				.setText(dust + (char) 0x00B0 + "C");

		String humidity = "";
		// Set humidity to humidity
		((TextView) rootView.findViewById(R.id.environment_humidity_tv))
				.setText(humidity + "%");

		String light = "";
		// Set light to light value
		((TextView) rootView.findViewById(R.id.environment_light_tv))
				.setText(light + "cd");

		String sound = "";
		// Set sound to sound value
		((TextView) rootView.findViewById(R.id.environment_sound_tv))
				.setText(sound + "dB");

		String temperature = "";
		// Set temperature to temperature
		((TextView) rootView.findViewById(R.id.environment_temperature_tv))
				.setText(temperature + (char) 0x00B0 + "C");

		return rootView;
	}
}
