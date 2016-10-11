using UnityEngine;

public class Cooldown
{
	float cdSeconds;
	float currentTime;

    bool finished = false;
    
	public Cooldown(float seconds)
	{
		cdSeconds = seconds;
		currentTime = 0.0f;
	}

    public Cooldown(float seconds, bool resetCD)
    {
        if (!resetCD)
        {
            cdSeconds = seconds;
            currentTime = seconds;
        }
        else
        {
            cdSeconds = seconds;
            currentTime = 0.0f;
        }
    }

	public void UpdateTimer()
	{
        if (!finished) currentTime += Time.deltaTime;
	}

	public void ResetTimer()
	{
		currentTime = 0.0f;
	}

	public bool ActionReady()
	{
		bool isReady = true;

		if (currentTime > cdSeconds)
		{
			isReady = true;
		}
		else 
		{
			isReady = false;
		}

		return isReady;
	}

    public void StopCooldown()
    {
        currentTime = 0.0f;
        finished = true;
    }
}
