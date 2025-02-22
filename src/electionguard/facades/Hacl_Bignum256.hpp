#ifndef __FACADES__Hacl_Bignum256_H_INCLUDED__
#define __FACADES__Hacl_Bignum256_H_INCLUDED__

#include "electionguard/export.h"

#include <cstdint>
#include <vector>

namespace hacl
{
    class EG_INTERNAL_API Bignum256
    {
      public:
        static uint64_t add(uint64_t *a, uint64_t *b, uint64_t *res);

        static uint64_t sub(uint64_t *a, uint64_t *b, uint64_t *res);

        static void mul(uint64_t *a, uint64_t *b, uint64_t *res);

        static bool mod(uint64_t *n, uint64_t *a, uint64_t *res);

        static bool modExp(uint64_t *n, uint64_t *a, uint32_t bBits, uint64_t *b, uint64_t *res);

        static uint64_t *fromBytes(uint32_t len, uint8_t *bytes);

        static std::vector<uint8_t> toBytes(uint64_t *bytes);
        static void toBytes(uint64_t *bytes, uint8_t *res);

        static uint64_t lessThan(uint64_t *a, uint64_t *b);
    };
} // namespace hacl

#endif /* __FACADES__Hacl_Bignum256_H_INCLUDED__ */
