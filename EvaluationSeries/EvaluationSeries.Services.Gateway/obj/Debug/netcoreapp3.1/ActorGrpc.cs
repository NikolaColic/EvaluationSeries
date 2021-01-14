// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: actor.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace EvaluationSeries.Grpc {
  public static partial class ActorsGrpc
  {
    static readonly string __ServiceName = "ActorsGrpc";

    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    static readonly grpc::Marshaller<global::EvaluationSeries.Grpc.ActorEmpty> __Marshaller_ActorEmpty = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::EvaluationSeries.Grpc.ActorEmpty.Parser));
    static readonly grpc::Marshaller<global::EvaluationSeries.Grpc.GetActorsResponse> __Marshaller_GetActorsResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::EvaluationSeries.Grpc.GetActorsResponse.Parser));
    static readonly grpc::Marshaller<global::EvaluationSeries.Grpc.ActorAdd> __Marshaller_ActorAdd = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::EvaluationSeries.Grpc.ActorAdd.Parser));
    static readonly grpc::Marshaller<global::EvaluationSeries.Grpc.ActorsMessageResponse> __Marshaller_ActorsMessageResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::EvaluationSeries.Grpc.ActorsMessageResponse.Parser));
    static readonly grpc::Marshaller<global::EvaluationSeries.Grpc.ActorId> __Marshaller_ActorId = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::EvaluationSeries.Grpc.ActorId.Parser));
    static readonly grpc::Marshaller<global::EvaluationSeries.Grpc.GetActorByIdResponse> __Marshaller_GetActorByIdResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::EvaluationSeries.Grpc.GetActorByIdResponse.Parser));

    static readonly grpc::Method<global::EvaluationSeries.Grpc.ActorEmpty, global::EvaluationSeries.Grpc.GetActorsResponse> __Method_GetActors = new grpc::Method<global::EvaluationSeries.Grpc.ActorEmpty, global::EvaluationSeries.Grpc.GetActorsResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetActors",
        __Marshaller_ActorEmpty,
        __Marshaller_GetActorsResponse);

    static readonly grpc::Method<global::EvaluationSeries.Grpc.ActorAdd, global::EvaluationSeries.Grpc.ActorsMessageResponse> __Method_PostActor = new grpc::Method<global::EvaluationSeries.Grpc.ActorAdd, global::EvaluationSeries.Grpc.ActorsMessageResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "PostActor",
        __Marshaller_ActorAdd,
        __Marshaller_ActorsMessageResponse);

    static readonly grpc::Method<global::EvaluationSeries.Grpc.ActorAdd, global::EvaluationSeries.Grpc.ActorsMessageResponse> __Method_PutActor = new grpc::Method<global::EvaluationSeries.Grpc.ActorAdd, global::EvaluationSeries.Grpc.ActorsMessageResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "PutActor",
        __Marshaller_ActorAdd,
        __Marshaller_ActorsMessageResponse);

    static readonly grpc::Method<global::EvaluationSeries.Grpc.ActorId, global::EvaluationSeries.Grpc.ActorsMessageResponse> __Method_DeleteActor = new grpc::Method<global::EvaluationSeries.Grpc.ActorId, global::EvaluationSeries.Grpc.ActorsMessageResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "DeleteActor",
        __Marshaller_ActorId,
        __Marshaller_ActorsMessageResponse);

    static readonly grpc::Method<global::EvaluationSeries.Grpc.ActorId, global::EvaluationSeries.Grpc.GetActorByIdResponse> __Method_GetActorsId = new grpc::Method<global::EvaluationSeries.Grpc.ActorId, global::EvaluationSeries.Grpc.GetActorByIdResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetActorsId",
        __Marshaller_ActorId,
        __Marshaller_GetActorByIdResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::EvaluationSeries.Grpc.ActorReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for ActorsGrpc</summary>
    public partial class ActorsGrpcClient : grpc::ClientBase<ActorsGrpcClient>
    {
      /// <summary>Creates a new client for ActorsGrpc</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public ActorsGrpcClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for ActorsGrpc that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public ActorsGrpcClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected ActorsGrpcClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected ActorsGrpcClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::EvaluationSeries.Grpc.GetActorsResponse GetActors(global::EvaluationSeries.Grpc.ActorEmpty request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetActors(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::EvaluationSeries.Grpc.GetActorsResponse GetActors(global::EvaluationSeries.Grpc.ActorEmpty request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetActors, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::EvaluationSeries.Grpc.GetActorsResponse> GetActorsAsync(global::EvaluationSeries.Grpc.ActorEmpty request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetActorsAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::EvaluationSeries.Grpc.GetActorsResponse> GetActorsAsync(global::EvaluationSeries.Grpc.ActorEmpty request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetActors, null, options, request);
      }
      public virtual global::EvaluationSeries.Grpc.ActorsMessageResponse PostActor(global::EvaluationSeries.Grpc.ActorAdd request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return PostActor(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::EvaluationSeries.Grpc.ActorsMessageResponse PostActor(global::EvaluationSeries.Grpc.ActorAdd request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_PostActor, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::EvaluationSeries.Grpc.ActorsMessageResponse> PostActorAsync(global::EvaluationSeries.Grpc.ActorAdd request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return PostActorAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::EvaluationSeries.Grpc.ActorsMessageResponse> PostActorAsync(global::EvaluationSeries.Grpc.ActorAdd request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_PostActor, null, options, request);
      }
      public virtual global::EvaluationSeries.Grpc.ActorsMessageResponse PutActor(global::EvaluationSeries.Grpc.ActorAdd request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return PutActor(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::EvaluationSeries.Grpc.ActorsMessageResponse PutActor(global::EvaluationSeries.Grpc.ActorAdd request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_PutActor, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::EvaluationSeries.Grpc.ActorsMessageResponse> PutActorAsync(global::EvaluationSeries.Grpc.ActorAdd request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return PutActorAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::EvaluationSeries.Grpc.ActorsMessageResponse> PutActorAsync(global::EvaluationSeries.Grpc.ActorAdd request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_PutActor, null, options, request);
      }
      public virtual global::EvaluationSeries.Grpc.ActorsMessageResponse DeleteActor(global::EvaluationSeries.Grpc.ActorId request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DeleteActor(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::EvaluationSeries.Grpc.ActorsMessageResponse DeleteActor(global::EvaluationSeries.Grpc.ActorId request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_DeleteActor, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::EvaluationSeries.Grpc.ActorsMessageResponse> DeleteActorAsync(global::EvaluationSeries.Grpc.ActorId request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DeleteActorAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::EvaluationSeries.Grpc.ActorsMessageResponse> DeleteActorAsync(global::EvaluationSeries.Grpc.ActorId request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_DeleteActor, null, options, request);
      }
      public virtual global::EvaluationSeries.Grpc.GetActorByIdResponse GetActorsId(global::EvaluationSeries.Grpc.ActorId request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetActorsId(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::EvaluationSeries.Grpc.GetActorByIdResponse GetActorsId(global::EvaluationSeries.Grpc.ActorId request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetActorsId, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::EvaluationSeries.Grpc.GetActorByIdResponse> GetActorsIdAsync(global::EvaluationSeries.Grpc.ActorId request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetActorsIdAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::EvaluationSeries.Grpc.GetActorByIdResponse> GetActorsIdAsync(global::EvaluationSeries.Grpc.ActorId request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetActorsId, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override ActorsGrpcClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new ActorsGrpcClient(configuration);
      }
    }

  }
}
#endregion
