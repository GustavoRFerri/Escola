using System;

public class Solution
{
    public static void Main()
    {
        int[] orderProcessTime = { 2, 4, 5, 1, 1 };
        int[] shiftDuration = { 1, 5, 1, 5, 2 };

        int[] result = GetRemainingOrders(
            orderProcessTime,
            shiftDuration
        );

        Console.WriteLine(string.Join(", ", result));
    }


    public static int[] GetRemainingOrders(
        int[] orderProcessTime,
        int[] shiftDuration)
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
                // Skip orders that have already been completed
                if (remainingTime[currentOrder] == 0)
                {
                    currentOrder++;
                    continue;
                }

                int timeUsed = Math.Min(
                    remainingTime[currentOrder],
                    availableTime
                );

                remainingTime[currentOrder] -= timeUsed;
                availableTime -= timeUsed;

                // Move to the next order if this one is completed
                if (remainingTime[currentOrder] == 0)
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

            result[shift] = remainingOrders;
        }

        return result;
    }

   
}