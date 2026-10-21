using System;
using System.Runtime.CompilerServices;


public class Program
{
    public static void Main(string[] args)
    {
        int[] orderProcessTime = { 2, 4, 5, 1, 1 };

        int[] shiftDuration = { 1, 5, 1, 5, 2 };

        int[] remainingOrders = GetRemainingOrders(orderProcessTime, shiftDuration);
    }


    public static int[] GetRemainingOrders(int[] orderProcessTime, int[] shiftDuration)
    {
        int n = orderProcessTime.Length;
        int[] remainingTime = (int[])orderProcessTime.Clone();
        int[] result = new int[shiftDuration.Length];

        int currentOrder = 0;

        for (int shift = 0; shift < shiftDuration.Length; shift++)
        {
            int availableTime = shiftDuration[shift];

            while (currentOrder < n && availableTime > 0)
            {
                //Skip orders that are already completed
                if (remainingTime[currentOrder] <= 0)
                {
                    currentOrder++;
                    continue;
                }


                // Process the current order for the available time or until it's completed
                int timeUsed = Math.Min(remainingTime[currentOrder], availableTime);
                availableTime -= timeUsed;
                remainingTime[currentOrder] -= timeUsed;


                // Move to the next order if the current one is completed
                if (remainingTime[currentOrder] <= 0)
                {
                    currentOrder++;
                }
            }

            // Count orders that are not yet completed
            int remainingOrders = 0;

            for (int i = 0; i < n; i++)
            {
                if (remainingTime[i] > 0)
                {
                    remainingOrders++;
                }
            }

            // Store the count of remaining orders for the current shift
            result[shift] = remainingOrders;

        }

        return result;
    }
}