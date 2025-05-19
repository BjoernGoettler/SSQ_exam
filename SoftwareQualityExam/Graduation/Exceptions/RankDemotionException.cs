using Graduation.Core;

namespace Graduation.Exceptions;

public class RankDemotionException : Exception
{
    public RankDemotionException(string message) : base(message)
    {
    }
}