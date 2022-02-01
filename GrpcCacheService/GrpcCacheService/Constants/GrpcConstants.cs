using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Caching.Memory;

namespace GrpcCacheService.Constants;

public static class GrpcConstants
{
    public static readonly Empty EmptyResult = new();
    public static readonly MemoryCacheEntryOptions MemoryCacheEntryOptions = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.High);
}
