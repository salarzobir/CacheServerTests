using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcCacheService.Constants;
using Microsoft.Extensions.Caching.Memory;

namespace GrpcCacheService.Services;

public class CacheService : Cache.CacheBase
{
    private readonly IMemoryCache _memoryCache;
    public CacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public override Task<GetReply> Get(GetRequest request, ServerCallContext context)
    {
        return Task.FromResult(new GetReply
        {
            Value = _memoryCache.Get<string>(request.Key)
        });
    }

    public override Task<Empty> Set(SetRequest request, ServerCallContext context)
    {
        _memoryCache.Set(request.Key, request.Value, GrpcConstants.MemoryCacheEntryOptions);
        return Task.FromResult(GrpcConstants.EmptyResult);
    }

    public override Task<Empty> Remove(RemoveRequest request, ServerCallContext context)
    {
        _memoryCache.Remove(request.Key);
        return Task.FromResult(GrpcConstants.EmptyResult);
    }
}
