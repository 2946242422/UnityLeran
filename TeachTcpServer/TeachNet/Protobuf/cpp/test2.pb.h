// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: test2.proto

#ifndef GOOGLE_PROTOBUF_INCLUDED_test2_2eproto
#define GOOGLE_PROTOBUF_INCLUDED_test2_2eproto

#include <limits>
#include <string>

#include <google/protobuf/port_def.inc>
#if PROTOBUF_VERSION < 3021000
#error This file was generated by a newer version of protoc which is
#error incompatible with your Protocol Buffer headers. Please update
#error your headers.
#endif
#if 3021001 < PROTOBUF_MIN_PROTOC_VERSION
#error This file was generated by an older version of protoc which is
#error incompatible with your Protocol Buffer headers. Please
#error regenerate this file with a newer version of protoc.
#endif

#include <google/protobuf/port_undef.inc>
#include <google/protobuf/io/coded_stream.h>
#include <google/protobuf/arena.h>
#include <google/protobuf/arenastring.h>
#include <google/protobuf/generated_message_util.h>
#include <google/protobuf/metadata_lite.h>
#include <google/protobuf/generated_message_reflection.h>
#include <google/protobuf/message.h>
#include <google/protobuf/repeated_field.h>  // IWYU pragma: export
#include <google/protobuf/extension_set.h>  // IWYU pragma: export
#include <google/protobuf/unknown_field_set.h>
// @@protoc_insertion_point(includes)
#include <google/protobuf/port_def.inc>
#define PROTOBUF_INTERNAL_EXPORT_test2_2eproto
PROTOBUF_NAMESPACE_OPEN
namespace internal {
class AnyMetadata;
}  // namespace internal
PROTOBUF_NAMESPACE_CLOSE

// Internal implementation detail -- do not use these members.
struct TableStruct_test2_2eproto {
  static const uint32_t offsets[];
};
extern const ::PROTOBUF_NAMESPACE_ID::internal::DescriptorTable descriptor_table_test2_2eproto;
namespace GameSystemTest {
class HeartMsg;
struct HeartMsgDefaultTypeInternal;
extern HeartMsgDefaultTypeInternal _HeartMsg_default_instance_;
}  // namespace GameSystemTest
PROTOBUF_NAMESPACE_OPEN
template<> ::GameSystemTest::HeartMsg* Arena::CreateMaybeMessage<::GameSystemTest::HeartMsg>(Arena*);
PROTOBUF_NAMESPACE_CLOSE
namespace GameSystemTest {

// ===================================================================

class HeartMsg final :
    public ::PROTOBUF_NAMESPACE_ID::Message /* @@protoc_insertion_point(class_definition:GameSystemTest.HeartMsg) */ {
 public:
  inline HeartMsg() : HeartMsg(nullptr) {}
  ~HeartMsg() override;
  explicit PROTOBUF_CONSTEXPR HeartMsg(::PROTOBUF_NAMESPACE_ID::internal::ConstantInitialized);

  HeartMsg(const HeartMsg& from);
  HeartMsg(HeartMsg&& from) noexcept
    : HeartMsg() {
    *this = ::std::move(from);
  }

  inline HeartMsg& operator=(const HeartMsg& from) {
    CopyFrom(from);
    return *this;
  }
  inline HeartMsg& operator=(HeartMsg&& from) noexcept {
    if (this == &from) return *this;
    if (GetOwningArena() == from.GetOwningArena()
  #ifdef PROTOBUF_FORCE_COPY_IN_MOVE
        && GetOwningArena() != nullptr
  #endif  // !PROTOBUF_FORCE_COPY_IN_MOVE
    ) {
      InternalSwap(&from);
    } else {
      CopyFrom(from);
    }
    return *this;
  }

  static const ::PROTOBUF_NAMESPACE_ID::Descriptor* descriptor() {
    return GetDescriptor();
  }
  static const ::PROTOBUF_NAMESPACE_ID::Descriptor* GetDescriptor() {
    return default_instance().GetMetadata().descriptor;
  }
  static const ::PROTOBUF_NAMESPACE_ID::Reflection* GetReflection() {
    return default_instance().GetMetadata().reflection;
  }
  static const HeartMsg& default_instance() {
    return *internal_default_instance();
  }
  static inline const HeartMsg* internal_default_instance() {
    return reinterpret_cast<const HeartMsg*>(
               &_HeartMsg_default_instance_);
  }
  static constexpr int kIndexInFileMessages =
    0;

  friend void swap(HeartMsg& a, HeartMsg& b) {
    a.Swap(&b);
  }
  inline void Swap(HeartMsg* other) {
    if (other == this) return;
  #ifdef PROTOBUF_FORCE_COPY_IN_SWAP
    if (GetOwningArena() != nullptr &&
        GetOwningArena() == other->GetOwningArena()) {
   #else  // PROTOBUF_FORCE_COPY_IN_SWAP
    if (GetOwningArena() == other->GetOwningArena()) {
  #endif  // !PROTOBUF_FORCE_COPY_IN_SWAP
      InternalSwap(other);
    } else {
      ::PROTOBUF_NAMESPACE_ID::internal::GenericSwap(this, other);
    }
  }
  void UnsafeArenaSwap(HeartMsg* other) {
    if (other == this) return;
    GOOGLE_DCHECK(GetOwningArena() == other->GetOwningArena());
    InternalSwap(other);
  }

  // implements Message ----------------------------------------------

  HeartMsg* New(::PROTOBUF_NAMESPACE_ID::Arena* arena = nullptr) const final {
    return CreateMaybeMessage<HeartMsg>(arena);
  }
  using ::PROTOBUF_NAMESPACE_ID::Message::CopyFrom;
  void CopyFrom(const HeartMsg& from);
  using ::PROTOBUF_NAMESPACE_ID::Message::MergeFrom;
  void MergeFrom( const HeartMsg& from) {
    HeartMsg::MergeImpl(*this, from);
  }
  private:
  static void MergeImpl(::PROTOBUF_NAMESPACE_ID::Message& to_msg, const ::PROTOBUF_NAMESPACE_ID::Message& from_msg);
  public:
  PROTOBUF_ATTRIBUTE_REINITIALIZES void Clear() final;
  bool IsInitialized() const final;

  size_t ByteSizeLong() const final;
  const char* _InternalParse(const char* ptr, ::PROTOBUF_NAMESPACE_ID::internal::ParseContext* ctx) final;
  uint8_t* _InternalSerialize(
      uint8_t* target, ::PROTOBUF_NAMESPACE_ID::io::EpsCopyOutputStream* stream) const final;
  int GetCachedSize() const final { return _impl_._cached_size_.Get(); }

  private:
  void SharedCtor(::PROTOBUF_NAMESPACE_ID::Arena* arena, bool is_message_owned);
  void SharedDtor();
  void SetCachedSize(int size) const final;
  void InternalSwap(HeartMsg* other);

  private:
  friend class ::PROTOBUF_NAMESPACE_ID::internal::AnyMetadata;
  static ::PROTOBUF_NAMESPACE_ID::StringPiece FullMessageName() {
    return "GameSystemTest.HeartMsg";
  }
  protected:
  explicit HeartMsg(::PROTOBUF_NAMESPACE_ID::Arena* arena,
                       bool is_message_owned = false);
  public:

  static const ClassData _class_data_;
  const ::PROTOBUF_NAMESPACE_ID::Message::ClassData*GetClassData() const final;

  ::PROTOBUF_NAMESPACE_ID::Metadata GetMetadata() const final;

  // nested types ----------------------------------------------------

  // accessors -------------------------------------------------------

  enum : int {
    kTimeFieldNumber = 1,
  };
  // int64 time = 1;
  void clear_time();
  int64_t time() const;
  void set_time(int64_t value);
  private:
  int64_t _internal_time() const;
  void _internal_set_time(int64_t value);
  public:

  // @@protoc_insertion_point(class_scope:GameSystemTest.HeartMsg)
 private:
  class _Internal;

  template <typename T> friend class ::PROTOBUF_NAMESPACE_ID::Arena::InternalHelper;
  typedef void InternalArenaConstructable_;
  typedef void DestructorSkippable_;
  struct Impl_ {
    int64_t time_;
    mutable ::PROTOBUF_NAMESPACE_ID::internal::CachedSize _cached_size_;
  };
  union { Impl_ _impl_; };
  friend struct ::TableStruct_test2_2eproto;
};
// ===================================================================


// ===================================================================

#ifdef __GNUC__
  #pragma GCC diagnostic push
  #pragma GCC diagnostic ignored "-Wstrict-aliasing"
#endif  // __GNUC__
// HeartMsg

// int64 time = 1;
inline void HeartMsg::clear_time() {
  _impl_.time_ = int64_t{0};
}
inline int64_t HeartMsg::_internal_time() const {
  return _impl_.time_;
}
inline int64_t HeartMsg::time() const {
  // @@protoc_insertion_point(field_get:GameSystemTest.HeartMsg.time)
  return _internal_time();
}
inline void HeartMsg::_internal_set_time(int64_t value) {
  
  _impl_.time_ = value;
}
inline void HeartMsg::set_time(int64_t value) {
  _internal_set_time(value);
  // @@protoc_insertion_point(field_set:GameSystemTest.HeartMsg.time)
}

#ifdef __GNUC__
  #pragma GCC diagnostic pop
#endif  // __GNUC__

// @@protoc_insertion_point(namespace_scope)

}  // namespace GameSystemTest

// @@protoc_insertion_point(global_scope)

#include <google/protobuf/port_undef.inc>
#endif  // GOOGLE_PROTOBUF_INCLUDED_GOOGLE_PROTOBUF_INCLUDED_test2_2eproto
