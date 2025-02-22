﻿using System;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;

namespace ElectionGuard
{
    internal enum Status
    {
        ELECTIONGUARD_STATUS_SUCCESS = 0,
        ELECTIONGUARD_STATUS_ERROR_INVALID_ARGUMENT,
        ELECTIONGUARD_STATUS_ERROR_OUT_OF_RANGE,
        ELECTIONGUARD_STATUS_ERROR_IO_ERROR,
        ELECTIONGUARD_STATUS_ERROR_BAD_ACCESS,
        ELECTIONGUARD_STATUS_ERROR_BAD_ALLOC,
        ELECTIONGUARD_STATUS_ERROR_ALREADY_EXISTS,
        ELECTIONGUARD_STATUS_ERROR_RUNTIME_ERROR,

        /// This code should always be the last code in the collection
        // so that the status codes string can be correctly derived
        ELECTIONGUARD_STATUS_UNKNOWN
    }

    /// <Summary>
    /// Enumeration used when marking a ballot as cast or spoiled
    /// </Summary>
    public enum BallotBoxState
    {
        /// <Summary>
        /// A ballot that has been explicitly cast
        /// </Summary>
        Cast = 1,
        /// <Summary>
        /// A ballot that has been explicitly spoiled
        /// </Summary>
        Spoiled = 2,
        /// <Summary>
        /// A ballot whose state is unknown to ElectionGuard and will not be included in any election results
        /// </Summary>
        Unknown = 999
    }

    /// <Summary>
    /// Enumeration for the type of ElectionType
    /// see: https://developers.google.com/elections-data/reference/election-type
    /// </Summary>
    public enum ElectionType
    {
        unknown = 0,
        general = 1,
        partisanPrimaryClosed = 2,
        partisanPrimaryOpen = 3,
        primary = 4,
        runoff = 5,
        special = 6,
        other = 7
    };

    /// <Summary>
    /// Enumeration for the type of geopolitical unit
    /// see: https://developers.google.com/elections-data/reference/reporting-unit-type
    /// </Summary>
    public enum ReportingUnitType
    {
        unknown = 0,
        ballotBatch = 1,
        ballotStyleArea = 2,
        borough = 3,
        city = 4,
        cityCouncil = 5,
        combinedPrecinct = 6,
        congressional = 7,
        country = 8,
        county = 9,
        countyCouncil = 10,
        dropBox = 11,
        judicial = 12,
        municipality = 13,
        polling_place = 14,
        precinct = 15,
        school = 16,
        special = 17,
        splitPrecinct = 18,
        state = 19,
        stateHouse = 20,
        stateSenate = 21,
        township = 22,
        utility = 23,
        village = 24,
        voteCenter = 25,
        ward = 26,
        water = 27,
        other = 28,
    };

    /// <Summary>
    /// Enumeration for the type of VoteVariationType
    /// see: https://developers.google.com/elections-data/reference/vote-variation
    /// </Summary>
    public enum VoteVariationType
    {
        unknown = 0,
        one_of_m = 1,
        approval = 2,
        borda = 3,
        cumulative = 4,
        majority = 5,
        n_of_m = 6,
        plurality = 7,
        proportional = 8,
        range = 9,
        rcv = 10,
        super_majority = 11,
        other = 12
    };

    internal static unsafe class NativeInterface
    {
        const string DllName = "electionguard";

        internal unsafe struct CharPtr { };

        #region Group

        internal static unsafe class ElementModP
        {
            internal unsafe struct ElementModPType { };

            internal class ElementModPHandle
                : ElectionguardSafeHandle<ElementModPType>
            {
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = ElementModP.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"ElementModP Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_element_mod_p_new")]
            internal static extern Status New(
                ulong* in_data, out ElementModPHandle handle);

            [DllImport(DllName, EntryPoint = "eg_element_mod_p_new_unchecked")]
            internal static extern Status NewUnchecked(
                ulong* in_data, out ElementModPHandle handle);

            [DllImport(DllName, EntryPoint = "eg_element_mod_p_free")]
            internal static extern Status Free(ElementModPHandle handle);

            [DllImport(DllName, EntryPoint = "eg_element_mod_p_get_data")]
            internal static extern Status GetData(
                ElementModPHandle handle, ulong** out_data, out UIntPtr out_size);

            [DllImport(DllName, EntryPoint = "eg_element_mod_p_to_hex")]
            internal static extern Status ToHex(
                ElementModPHandle handle, out IntPtr data);
        }

        internal static unsafe class ElementModQ
        {
            internal unsafe struct ElementModQType { };

            internal class ElementModQHandle
                : ElectionguardSafeHandle<ElementModQType>
            {
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = ElementModQ.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"ElementModQ Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_element_mod_q_new")]
            internal static extern Status New(
                ulong* in_data, out ElementModQHandle handle);

            [DllImport(DllName, EntryPoint = "eg_element_mod_q_new_unchecked")]
            internal static extern Status NewUnchecked(
                ulong* in_data, out ElementModQHandle handle);

            [DllImport(DllName, EntryPoint = "eg_element_mod_q_free")]
            internal static extern Status Free(ElementModQHandle handle);

            [DllImport(DllName, EntryPoint = "eg_element_mod_q_get_data")]
            internal static extern Status GetData(
                ElementModQHandle handle, ulong** out_data, out UIntPtr out_size);

            [DllImport(DllName, EntryPoint = "eg_element_mod_q_to_hex")]
            internal static extern Status ToHex(
                ElementModQHandle handle, out IntPtr out_hex);

            [DllImport(DllName, EntryPoint = "eg_element_mod_q_rand_q_new")]
            internal static extern Status Random(out ElementModQHandle handle);
        }

        internal static unsafe class Constants
        {
            [DllImport(DllName, EntryPoint = "eg_element_mod_p_constant_g")]
            internal static extern Status G(out ElementModP.ElementModPHandle handle);

            [DllImport(DllName, EntryPoint = "eg_element_mod_p_constant_p")]
            internal static extern Status P(out ElementModP.ElementModPHandle handle);

            [DllImport(DllName, EntryPoint = "eg_element_mod_p_constant_zero_mod_p")]
            internal static extern Status ZERO_MOD_P(out ElementModP.ElementModPHandle handle);

            [DllImport(DllName, EntryPoint = "eg_element_mod_p_constant_one_mod_p")]
            internal static extern Status ONE_MOD_P(out ElementModP.ElementModPHandle handle);

            [DllImport(DllName, EntryPoint = "eg_element_mod_p_constant_two_mod_p")]
            internal static extern Status TWO_MOD_P(out ElementModP.ElementModPHandle handle);

            [DllImport(DllName, EntryPoint = "eg_element_mod_p_constant_p")]
            internal static extern Status Q(out ElementModQ.ElementModQHandle handle);

            [DllImport(DllName, EntryPoint = "eg_element_mod_q_constant_zero_mod_q")]
            internal static extern Status ZERO_MOD_Q(out ElementModQ.ElementModQHandle handle);

            [DllImport(DllName, EntryPoint = "eg_element_mod_q_constant_one_mod_q")]
            internal static extern Status ONE_MOD_Q(out ElementModQ.ElementModQHandle handle);

            [DllImport(DllName, EntryPoint = "eg_element_mod_q_constant_two_mod_q")]
            internal static extern Status TWO_MOD_Q(out ElementModQ.ElementModQHandle handle);
        }

        #endregion

        #region Elgamal

        internal static unsafe class ElGamalKeyPair
        {
            internal unsafe struct ElGamalKeyPairType { };

            internal class ElGamalKeyPairHandle
                : ElectionguardSafeHandle<ElGamalKeyPairType>
            {
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = ElGamalKeyPair.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"ElGamalKeyPair Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_elgamal_keypair_from_secret_new")]
            internal static extern Status New(
                ElementModQ.ElementModQHandle in_secret_key,
                out ElGamalKeyPairHandle handle);

            [DllImport(DllName, EntryPoint = "eg_elgamal_keypair_free")]
            internal static extern Status Free(ElGamalKeyPairHandle handle);

            [DllImport(DllName, EntryPoint = "eg_elgamal_keypair_get_public_key")]
            internal static extern Status GetPublicKey(
                ElGamalKeyPairHandle handle,
                out ElementModP.ElementModPHandle out_public_key);

            [DllImport(DllName, EntryPoint = "eg_elgamal_keypair_get_secret_key")]
            internal static extern Status GetSecretKey(
                ElGamalKeyPairHandle handle,
                out ElementModQ.ElementModQHandle out_secret_key);

        }

        internal static unsafe class ElGamalCiphertext
        {
            internal unsafe struct ElGamalCiphertextType { };

            internal class ElGamalCiphertextHandle
                : ElectionguardSafeHandle<ElGamalCiphertextType>
            {
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = ElGamalCiphertext.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"ElGamalCiphertext Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_elgamal_ciphertext_free")]
            internal static extern Status Free(ElGamalCiphertextHandle handle);

            [DllImport(DllName, EntryPoint = "eg_elgamal_ciphertext_get_pad")]
            internal static extern Status GetPad(
                ElGamalCiphertextHandle handle,
                out ElementModP.ElementModPHandle elgamal_public_key);

            [DllImport(DllName, EntryPoint = "eg_elgamal_ciphertext_get_data")]
            internal static extern Status GetData(
                ElGamalCiphertextHandle handle,
                out ElementModP.ElementModPHandle elgamal_public_key);

            [DllImport(DllName, EntryPoint = "eg_elgamal_ciphertext_crypto_hash")]
            internal static extern Status GetCryptoHash(
                ElGamalCiphertextHandle handle,
                out ElementModQ.ElementModQHandle crypto_base_hash);

            [DllImport(DllName, EntryPoint = "eg_elgamal_ciphertext_decrypt_with_secret")]
            internal static extern Status DecryptWithSecret(
                ElGamalCiphertextHandle handle,
                ElementModQ.ElementModQHandle secret_key,
                ref ulong plaintext);

        }

        internal static unsafe class ElGamal
        {
            [DllImport(DllName, EntryPoint = "eg_elgamal_encrypt")]
            internal static extern Status Encrypt(
                ulong plaintext,
                ElementModQ.ElementModQHandle nonce,
                ElementModP.ElementModPHandle public_key,
                out ElGamalCiphertext.ElGamalCiphertextHandle handle);
        }

        #endregion

        #region ChaumPedersen

        internal static unsafe class DisjunctiveChaumPedersenProof
        {
            internal unsafe struct DisjunctiveChaumPedersenProofType { };

            internal class DisjunctiveChaumPedersenProofHandle
                : ElectionguardSafeHandle<DisjunctiveChaumPedersenProofType>
            {
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = DisjunctiveChaumPedersenProof.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"DisjunctiveChaumPedersenProof Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_disjunctive_chaum_pedersen_proof_free")]
            internal static extern Status Free(DisjunctiveChaumPedersenProofHandle handle);

            [DllImport(DllName, EntryPoint = "eg_disjunctive_chaum_pedersen_proof_get_zero_pad")]
            internal static extern Status GetZeroPad(
                DisjunctiveChaumPedersenProofHandle handle,
                out ElementModP.ElementModPHandle element);

            [DllImport(DllName, EntryPoint = "eg_disjunctive_chaum_pedersen_proof_get_zero_data")]
            internal static extern Status GetZeroData(
                DisjunctiveChaumPedersenProofHandle handle,
                out ElementModP.ElementModPHandle element);

            [DllImport(DllName, EntryPoint = "eg_disjunctive_chaum_pedersen_proof_get_one_pad")]
            internal static extern Status GetOnePad(
                DisjunctiveChaumPedersenProofHandle handle,
                out ElementModP.ElementModPHandle element);

            [DllImport(DllName, EntryPoint = "eg_disjunctive_chaum_pedersen_proof_get_one_data")]
            internal static extern Status GetOneData(
                DisjunctiveChaumPedersenProofHandle handle,
                out ElementModP.ElementModPHandle element);

            [DllImport(DllName, EntryPoint = "eg_disjunctive_chaum_pedersen_proof_get_zero_challenge")]
            internal static extern Status GetZeroChallenge(
                DisjunctiveChaumPedersenProofHandle handle,
                out ElementModQ.ElementModQHandle element);

            [DllImport(DllName, EntryPoint = "eg_disjunctive_chaum_pedersen_proof_get_one_challenge")]
            internal static extern Status GetOneChallenge(
                DisjunctiveChaumPedersenProofHandle handle,
                out ElementModQ.ElementModQHandle element);

            [DllImport(DllName, EntryPoint = "eg_disjunctive_chaum_pedersen_proof_get_challenge")]
            internal static extern Status GetChallenge(
                DisjunctiveChaumPedersenProofHandle handle,
                out ElementModQ.ElementModQHandle element);

            [DllImport(DllName, EntryPoint = "eg_disjunctive_chaum_pedersen_proof_get_zero_response")]
            internal static extern Status GetZeroResponse(
                DisjunctiveChaumPedersenProofHandle handle,
                out ElementModQ.ElementModQHandle element);

            [DllImport(DllName, EntryPoint = "eg_disjunctive_chaum_pedersen_proof_get_one_response")]
            internal static extern Status GetOneResponse(
                DisjunctiveChaumPedersenProofHandle handle,
                out ElementModQ.ElementModQHandle element);

            [DllImport(DllName, EntryPoint = "eg_disjunctive_chaum_pedersen_proof_make")]
            internal static extern Status Make(
                ElGamalCiphertext.ElGamalCiphertextHandle message,
                ElementModQ.ElementModQHandle r,
                ElementModP.ElementModPHandle k,
                ElementModQ.ElementModQHandle q,
                ElementModQ.ElementModQHandle seed,
                ulong plaintext,
                out DisjunctiveChaumPedersenProofHandle handle);

            [DllImport(DllName, EntryPoint = "eg_disjunctive_chaum_pedersen_proof_is_valid")]
            internal static extern bool IsValid(
                DisjunctiveChaumPedersenProofHandle handle,
                ElGamalCiphertext.ElGamalCiphertextHandle message,
                ElementModP.ElementModPHandle k,
                ElementModQ.ElementModQHandle q);

        }

        internal static unsafe class ConstantChaumPedersenProof
        {
            internal unsafe struct ConstantChaumPedersenProofType { };

            internal class ConstantChaumPedersenProofHandle
                : ElectionguardSafeHandle<ConstantChaumPedersenProofType>
            {
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = ConstantChaumPedersenProof.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"ConstantChaumPedersenProof Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_constant_chaum_pedersen_proof_free")]
            internal static extern Status Free(ConstantChaumPedersenProofHandle handle);

            [DllImport(DllName, EntryPoint = "eg_constant_chaum_pedersen_proof_get_pad")]
            internal static extern Status GetPad(
                ConstantChaumPedersenProofHandle handle,
                out ElementModP.ElementModPHandle element);

            [DllImport(DllName, EntryPoint = "eg_constant_chaum_pedersen_proof_get_data")]
            internal static extern Status GetData(
                ConstantChaumPedersenProofHandle handle,
                out ElementModP.ElementModPHandle element);

            [DllImport(DllName, EntryPoint = "eg_constant_chaum_pedersen_proof_get_challenge")]
            internal static extern Status GetChallenge(
                ConstantChaumPedersenProofHandle handle,
                out ElementModQ.ElementModQHandle element);

            [DllImport(DllName, EntryPoint = "eg_constant_chaum_pedersen_proof_get_response")]
            internal static extern Status GetResponse(
                ConstantChaumPedersenProofHandle handle,
                out ElementModQ.ElementModQHandle element);

            [DllImport(DllName, EntryPoint = "eg_constant_chaum_pedersen_proof_make")]
            internal static extern Status Make(
                ElGamalCiphertext.ElGamalCiphertextHandle message,
                ElementModQ.ElementModQHandle r,
                ElementModP.ElementModPHandle k,
                ElementModQ.ElementModQHandle seed,
                ElementModQ.ElementModQHandle hash_header,
                ulong constant,
                out ConstantChaumPedersenProofHandle handle);

            [DllImport(DllName, EntryPoint = "eg_constant_chaum_pedersen_proof_is_valid")]
            internal static extern bool IsValid(
                ConstantChaumPedersenProofHandle handle,
                ElGamalCiphertext.ElGamalCiphertextHandle message,
                ElementModP.ElementModPHandle k,
                ElementModQ.ElementModQHandle q);

        }


        #endregion

        #region AnnotatedString

        internal static unsafe class AnnotatedString
        {
            internal unsafe struct AnnotatedStringType { };

            internal class AnnotatedStringHandle
                : ElectionguardSafeHandle<AnnotatedStringType>
            {
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = AnnotatedString.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"AnnotatedString Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_annotated_string_new")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string annotation,
                [MarshalAs(UnmanagedType.LPStr)] string value,
                out AnnotatedStringHandle handle);

            [DllImport(DllName, EntryPoint = "eg_annotated_string_free")]
            internal static extern Status Free(AnnotatedStringHandle handle);

            [DllImport(DllName, EntryPoint = "eg_annotated_string_get_annotation")]
            internal static extern Status GetAnnotation(
                AnnotatedStringHandle handle, out IntPtr language);


            [DllImport(DllName, EntryPoint = "eg_annotated_string_get_value")]
            internal static extern Status GetValue(
                AnnotatedStringHandle handle, out IntPtr value);

            [DllImport(DllName, EntryPoint = "eg_annotated_string_crypto_hash")]
            internal static extern Status CryptoHash(
                AnnotatedStringHandle handle,
                out ElementModQ.ElementModQHandle crypto_hash);
        }

        #endregion

        #region Language

        internal static unsafe class Language
        {
            internal unsafe struct LanguageType { };

            internal class LanguageHandle
                : ElectionguardSafeHandle<LanguageType>
            {
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = Language.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"Language Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_language_new")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string value,
                [MarshalAs(UnmanagedType.LPStr)] string language,
                out LanguageHandle handle);

            [DllImport(DllName, EntryPoint = "eg_language_free")]
            internal static extern Status Free(LanguageHandle handle);

            [DllImport(DllName, EntryPoint = "eg_language_get_value")]
            internal static extern Status GetValue(
                LanguageHandle handle, out IntPtr value);

            [DllImport(DllName, EntryPoint = "eg_language_get_language")]
            internal static extern Status GetLanguage(
                LanguageHandle handle, out IntPtr language);

            [DllImport(DllName, EntryPoint = "eg_language_crypto_hash")]
            internal static extern Status CryptoHash(
                LanguageHandle handle,
                out ElementModQ.ElementModQHandle crypto_hash);
        }

        #endregion

        #region InternationalizedText

        internal static unsafe class InternationalizedText
        {
            internal unsafe struct InternationalizedTextType { };

            internal class InternationalizedTextHandle
                : ElectionguardSafeHandle<InternationalizedTextType>
            {
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = InternationalizedText.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"InternationalizedText Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_internationalized_text_new")]
            internal static extern Status New(
                // TODO: type safety
                [MarshalAs(UnmanagedType.LPArray)] IntPtr[] text,
                long textSize,
                out InternationalizedTextHandle handle);

            [DllImport(DllName, EntryPoint = "eg_internationalized_text_free")]
            internal static extern Status Free(InternationalizedTextHandle handle);

            [DllImport(DllName, EntryPoint = "eg_internationalized_text_get_text_size")]
            internal static extern ulong GetTextSize(
                InternationalizedTextHandle handle);

            [DllImport(DllName, EntryPoint = "eg_internationalized_text_get_text_at_index")]
            internal static extern Status GetTextAtIndex(
                InternationalizedTextHandle handle,
                ulong index,
                out Language.LanguageHandle text);

            [DllImport(DllName, EntryPoint = "eg_intertnationalized_text_crypto_hash")]
            internal static extern Status CryptoHash(
                InternationalizedTextHandle handle,
                out ElementModQ.ElementModQHandle crypto_hash);
        }

        #endregion

        #region ContactInformation

        internal static unsafe class ContactInformation
        {
            internal unsafe struct ContactInformationType { };

            internal class ContactInformationHandle
                : ElectionguardSafeHandle<ContactInformationType>
            {
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = ContactInformation.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"ContactInformation Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_contact_information_new")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string name,
                out ContactInformationHandle handle);

            // TODO: add eg_contact_information_new_with_collections

            [DllImport(DllName, EntryPoint = "eg_contact_information_free")]
            internal static extern Status Free(ContactInformationHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contact_information_get_address_line_size")]
            internal static extern ulong GetAddressLineSize(
                ContactInformationHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contact_information_get_address_line_at_index")]
            internal static extern Status GetAddressLineAtIndex(
                ContactInformationHandle handle,
                ulong index,
                out IntPtr address);

            [DllImport(DllName, EntryPoint = "eg_contact_information_get_email_line_size")]
            internal static extern ulong GetEmailLineSize(
                ContactInformationHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contact_information_get_email_line_at_index")]
            internal static extern Status GetEmailLineAtIndex(
                ContactInformationHandle handle,
                ulong index,
                out InternationalizedText.InternationalizedTextHandle email);

            [DllImport(DllName, EntryPoint = "eg_contact_information_get_phone_line_size")]
            internal static extern ulong GetPhoneLineSize(
                ContactInformationHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contact_information_get_email_line_at_index")]
            internal static extern Status GetPhoneLineAtIndex(
                ContactInformationHandle handle,
                ulong index,
                out InternationalizedText.InternationalizedTextHandle phone);

            [DllImport(DllName, EntryPoint = "eg_contact_information_get_name")]
            internal static extern Status GetName(
                ContactInformationHandle handle, out IntPtr value);

            [DllImport(DllName, EntryPoint = "eg_contact_information_crypto_hash")]
            internal static extern Status CryptoHash(
                ContactInformationHandle handle,
                out ElementModQ.ElementModQHandle crypto_hash);
        }

        #endregion

        #region GeopoliticalUnit

        internal static unsafe class GeopoliticalUnit
        {
            internal unsafe struct GeopoliticalUnitType { };

            internal class GeopoliticalUnitHandle
                : ElectionguardSafeHandle<GeopoliticalUnitType>
            {
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = GeopoliticalUnit.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"GeopoliticalUnit Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_geopolitical_unit_new")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string objectId,
                [MarshalAs(UnmanagedType.LPStr)] string name,
                ReportingUnitType reportingUnitType,
                out GeopoliticalUnitHandle handle);

            [DllImport(DllName, EntryPoint = "eg_geopolitical_unit_new_with_contact_info")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string objectId,
                [MarshalAs(UnmanagedType.LPStr)] string name,
                ReportingUnitType reportingUnitType,
                ContactInformation.ContactInformationHandle contactInformation,
                out GeopoliticalUnitHandle handle);

            [DllImport(DllName, EntryPoint = "eg_geopolitical_unit_free")]
            internal static extern Status Free(GeopoliticalUnitHandle handle);

            [DllImport(DllName, EntryPoint = "eg_geopolitical_unit_get_object_id")]
            internal static extern Status GetObjectId(
                GeopoliticalUnitHandle handle, out IntPtr objectId);

            [DllImport(DllName, EntryPoint = "eg_geopolitical_unit_get_name")]
            internal static extern Status GetName(
                GeopoliticalUnitHandle handle, out IntPtr name);

            [DllImport(DllName, EntryPoint = "get_geopolitical_unit_get_type")]
            internal static extern ReportingUnitType GetReportingUnitType(
                GeopoliticalUnitHandle handle);

            [DllImport(DllName, EntryPoint = "eg_geopolitical_unit_crypto_hash")]
            internal static extern Status CryptoHash(
                GeopoliticalUnitHandle handle,
                out ElementModQ.ElementModQHandle crypto_hash);
        }

        #endregion

        #region BallotStyle

        internal static unsafe class BallotStyle
        {
            internal unsafe struct BallotStyleType { };

            internal class BallotStyleHandle
                : ElectionguardSafeHandle<BallotStyleType>
            {
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = BallotStyle.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"BallotStyle Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_ballot_style_new")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string objectId,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] gpUnitIds,
                long gpUnitIdsSize,
                out BallotStyleHandle handle);

            // TODO eg_ballot_style_new_with_parties

            [DllImport(DllName, EntryPoint = "eg_ballot_style_free")]
            internal static extern Status Free(BallotStyleHandle handle);

            [DllImport(DllName, EntryPoint = "eg_ballot_style_get_object_id")]
            internal static extern Status GetObjectId(
                BallotStyleHandle handle, out IntPtr objectId);

            [DllImport(DllName, EntryPoint = "eg_ballot_style_get_geopolitical_unit_ids_size")]
            internal static extern ulong GetGeopoliticalUnitSize(
                BallotStyleHandle handle);

            [DllImport(DllName, EntryPoint = "eg_ballot_style_get_geopolitical_unit_id_at_index")]
            internal static extern Status GetGeopoliticalInitIdAtIndex(
                BallotStyleHandle handle,
                ulong index,
                out IntPtr gpUnitId);

            [DllImport(DllName, EntryPoint = "eg_ballot_style_get_party_ids_size")]
            internal static extern ulong GetPartyIdsSize(
                BallotStyleHandle handle);

            [DllImport(DllName, EntryPoint = "eg_ballot_style_get_party_id_at_index")]
            internal static extern Status GetPartyIdAtIndex(
                BallotStyleHandle handle,
                ulong index,
                out IntPtr partyId);

            [DllImport(DllName, EntryPoint = "eg_ballot_style_get_image_uri")]
            internal static extern Status GetImageUri(
                BallotStyleHandle handle, out IntPtr imageUri);

            [DllImport(DllName, EntryPoint = "eg_ballot_style_crypto_hash")]
            internal static extern Status CryptoHash(
                BallotStyleHandle handle,
                out ElementModQ.ElementModQHandle crypto_hash);
        }

        #endregion

        #region Party

        internal static unsafe class Party
        {
            internal unsafe struct PartyType { };

            internal class PartyHandle
                : ElectionguardSafeHandle<PartyType>
            {
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = Party.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"Party Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_party_new")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string objectId,
                out PartyHandle handle);

            [DllImport(DllName, EntryPoint = "eg_party_new_with_extras")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string objectId,
                InternationalizedText.InternationalizedTextHandle name,
                [MarshalAs(UnmanagedType.LPStr)] string abbreviation,
                [MarshalAs(UnmanagedType.LPStr)] string color,
                [MarshalAs(UnmanagedType.LPStr)] string logoUri,
                out PartyHandle handle);

            [DllImport(DllName, EntryPoint = "eg_party_free")]
            internal static extern Status Free(PartyHandle handle);

            [DllImport(DllName, EntryPoint = "eg_party_get_object_id")]
            internal static extern Status GetObjectId(
                PartyHandle handle, out IntPtr objectId);

            [DllImport(DllName, EntryPoint = "eg_party_get_abbreviation")]
            internal static extern Status GetAbbreviation(
                PartyHandle handle, out IntPtr abbreviation);

            [DllImport(DllName, EntryPoint = "eg_party_get_name")]
            internal static extern Status GetName(
                PartyHandle handle, out InternationalizedText.InternationalizedTextHandle name);

            [DllImport(DllName, EntryPoint = "eg_party_get_color")]
            internal static extern Status GetColor(
                PartyHandle handle, out IntPtr color);

            [DllImport(DllName, EntryPoint = "eg_party_get_logo_uri")]
            internal static extern Status GetLogoUri(
                PartyHandle handle, out IntPtr logoUri);

            [DllImport(DllName, EntryPoint = "eg_party_crypto_hash")]
            internal static extern Status CryptoHash(
                PartyHandle handle,
                out ElementModQ.ElementModQHandle crypto_hash);
        }

        #endregion

        #region Candidate

        internal static unsafe class Candidate
        {
            internal unsafe struct CandidateType { };

            internal class CandidateHandle
                : ElectionguardSafeHandle<CandidateType>
            {
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = Candidate.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"Candidate Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_candidate_new")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string objectId,
                bool isWriteIn,
                out CandidateHandle handle);

            [DllImport(DllName, EntryPoint = "eg_candidate_new_with_party")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string objectId,
                [MarshalAs(UnmanagedType.LPStr)] string partyId,
                bool isWriteIn,
                out CandidateHandle handle);

            [DllImport(DllName, EntryPoint = "eg_candidate_new_with_extras")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string objectId,
                InternationalizedText.InternationalizedTextHandle name,
                [MarshalAs(UnmanagedType.LPStr)] string partyId,
                [MarshalAs(UnmanagedType.LPStr)] string imageUri,
                bool isWriteIn,
                out CandidateHandle handle);

            [DllImport(DllName, EntryPoint = "eg_candidate_free")]
            internal static extern Status Free(CandidateHandle handle);

            [DllImport(DllName, EntryPoint = "eg_candidate_get_object_id")]
            internal static extern Status GetObjectId(
                CandidateHandle handle, out IntPtr objectId);

            [DllImport(DllName, EntryPoint = "eg_candidate_get_candidate_id")]
            internal static extern Status GetCandidateId(
                CandidateHandle handle, out IntPtr candidateId);

            [DllImport(DllName, EntryPoint = "eg_candidate_get_name")]
            internal static extern Status GetName(
                CandidateHandle handle, out IntPtr name);

            [DllImport(DllName, EntryPoint = "eg_candidate_get_party_id")]
            internal static extern Status GetPartyId(
                CandidateHandle handle, out IntPtr partyId);

            [DllImport(DllName, EntryPoint = "eg_candidate_get_image_uri")]
            internal static extern Status GetImageUri(
                CandidateHandle handle, out IntPtr iamgeUri);

            [DllImport(DllName, EntryPoint = "eg_candidate_crypto_hash")]
            internal static extern Status CryptoHash(
                CandidateHandle handle,
                out ElementModQ.ElementModQHandle crypto_hash);
        }

        #endregion

        #region SelectionDescription

        internal static unsafe class SelectionDescription
        {
            internal unsafe struct SelectionDescriptionType { };

            internal class SelectionDescriptionHandle
                : ElectionguardSafeHandle<SelectionDescriptionType>
            {
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = SelectionDescription.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"SelectionDescription Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_selection_description_new")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string objectId,
                [MarshalAs(UnmanagedType.LPStr)] string candidateId,
                ulong sequenceOrder,
                out SelectionDescriptionHandle handle);

            [DllImport(DllName, EntryPoint = "eg_selection_description_free")]
            internal static extern Status Free(SelectionDescriptionHandle handle);

            [DllImport(DllName, EntryPoint = "eg_selection_description_get_object_id")]
            internal static extern Status GetObjectId(
                SelectionDescriptionHandle handle, out IntPtr objectId);

            [DllImport(DllName, EntryPoint = "eg_selection_description_get_candidate_id")]
            internal static extern Status GetCandidateId(
                SelectionDescriptionHandle handle, out IntPtr candidateId);

            [DllImport(DllName, EntryPoint = "eg_selection_description_get_sequence_order")]
            internal static extern ulong GetSequenceOrder(
                SelectionDescriptionHandle handle);

            [DllImport(DllName, EntryPoint = "eg_selection_description_crypto_hash")]
            internal static extern Status CryptoHash(
                SelectionDescriptionHandle handle,
                out ElementModQ.ElementModQHandle crypto_hash);
        }

        #endregion

        #region ContestDescription

        internal static unsafe class ContestDescription
        {
            internal unsafe struct ContestDescriptionType { };

            internal class ContestDescriptionHandle
                : ElectionguardSafeHandle<ContestDescriptionType>
            {
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = ContestDescription.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"ContestDescription Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_contest_description_new")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string objectId,
                [MarshalAs(UnmanagedType.LPStr)] string electoralDistrictId,
                ulong sequenceOrder,
                VoteVariationType voteVariation,
                ulong numberElected,
                [MarshalAs(UnmanagedType.LPStr)] string name,
                // TODO: type safety
                [MarshalAs(UnmanagedType.LPArray)] IntPtr[] selections,
                ulong selectionsSize,
                out ContestDescriptionHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contest_description_new_with_title")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string objectId,
                [MarshalAs(UnmanagedType.LPStr)] string electoralDistrictId,
                ulong sequenceOrder,
                VoteVariationType voteVariation,
                ulong numberElected,
                ulong votesAllowed,
                [MarshalAs(UnmanagedType.LPStr)] string name,
                InternationalizedText.InternationalizedTextHandle ballotTitle,
                InternationalizedText.InternationalizedTextHandle ballotSubTitle,
                // TODO ISSUE #212: type safety
                [MarshalAs(UnmanagedType.LPArray)] IntPtr[] selections,
                ulong selectionsSize,
                out ContestDescriptionHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contest_description_new_with_parties")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string objectId,
                [MarshalAs(UnmanagedType.LPStr)] string electoralDistrictId,
                ulong sequenceOrder,
                VoteVariationType voteVariation,
                ulong numberElected,
                [MarshalAs(UnmanagedType.LPStr)] string name,
                // TODO ISSUE #212: type safety
                [MarshalAs(UnmanagedType.LPArray)] IntPtr[] selections,
                ulong selectionsSize,
                // TODO ISSUE #212: type safety
                [MarshalAs(UnmanagedType.LPArray)] string[] primaryPartyIds,
                ulong primaryPartyIdsSize,
                out ContestDescriptionHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contest_description_new_with_title_and_parties")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string objectId,
                [MarshalAs(UnmanagedType.LPStr)] string electoralDistrictId,
                ulong sequenceOrder,
                VoteVariationType voteVariation,
                ulong numberElected,
                ulong votesAllowed,
                [MarshalAs(UnmanagedType.LPStr)] string name,
                InternationalizedText.InternationalizedTextHandle ballotTitle,
                InternationalizedText.InternationalizedTextHandle ballotSubTitle,
                // TODO ISSUE #212: type safety
                [MarshalAs(UnmanagedType.LPArray)] IntPtr[] selections,
                ulong selectionsSize,
                // TODO ISSUE #212: type safety
                [MarshalAs(UnmanagedType.LPArray)] string[] primaryPartyIds,
                ulong primaryPartyIdsSize,
                out ContestDescriptionHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contest_description_free")]
            internal static extern Status Free(ContestDescriptionHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contest_description_get_object_id")]
            internal static extern Status GetObjectId(
                ContestDescriptionHandle handle, out IntPtr objectId);

            [DllImport(DllName, EntryPoint = "eg_contest_description_get_electoral_district_id")]
            internal static extern Status GetElectoralDistrictId(
                ContestDescriptionHandle handle, out IntPtr electoralDistrictId);

            [DllImport(DllName, EntryPoint = "eg_contest_description_get_sequence_order")]
            internal static extern ulong GetSequenceOrder(
                ContestDescriptionHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contest_description_get_vote_variation")]
            internal static extern VoteVariationType GetVoteVariationType(
                ContestDescriptionHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contest_description_get_number_elected")]
            internal static extern ulong GetNumberElected(
                ContestDescriptionHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contest_description_get_votes_allowed")]
            internal static extern ulong GetVotesAllowed(
                ContestDescriptionHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contest_description_get_name")]
            internal static extern Status GetName(
                ContestDescriptionHandle handle, out IntPtr name);

            [DllImport(DllName, EntryPoint = "eg_contest_description_get_ballot_title")]
            internal static extern Status GetBallotTitle(
                ContestDescriptionHandle handle, out InternationalizedText.InternationalizedTextHandle name);

            [DllImport(DllName, EntryPoint = "eg_contest_description_get_ballot_subtitle")]
            internal static extern Status GetBallotSubTitle(
                ContestDescriptionHandle handle, out InternationalizedText.InternationalizedTextHandle name);

            [DllImport(DllName, EntryPoint = "eg_contest_description_get_selections_size")]
            internal static extern ulong GetSelectionsSize(
                ContestDescriptionHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contest_description_get_selection_at_index")]
            internal static extern Status GetSelectionAtIndex(
                ContestDescriptionHandle handle,
                ulong index,
                out SelectionDescription.SelectionDescriptionHandle partyId);

            [DllImport(DllName, EntryPoint = "eg_contest_description_crypto_hash")]
            internal static extern Status CryptoHash(
                ContestDescriptionHandle handle,
                out ElementModQ.ElementModQHandle crypto_hash);
        }

        #endregion

        #region ContestDescriptionWithPlaceholders

        internal static unsafe class ContestDescriptionWithPlaceholders
        {
            internal unsafe struct ContestDescriptionWithPlaceholdersType { };

            internal class ContestDescriptionWithPlaceholdersHandle
                : ElectionguardSafeHandle<ContestDescriptionWithPlaceholdersType>
            {
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = ContestDescriptionWithPlaceholders.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"ContestDescriptionWithPlaceholders Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_contest_description_with_placeholders_new")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string objectId,
                [MarshalAs(UnmanagedType.LPStr)] string electoralDistrictId,
                ulong sequenceOrder,
                VoteVariationType voteVariation,
                ulong numberElected,
                [MarshalAs(UnmanagedType.LPStr)] string name,
                // TODO ISSUE #212: type safety
                [MarshalAs(UnmanagedType.LPArray)] IntPtr[] selections,
                ulong selectionsSize,
                // TODO ISSUE #212: type safety
                [MarshalAs(UnmanagedType.LPArray)] IntPtr[] placeholders,
                ulong placeholdersSize,
                out ContestDescriptionWithPlaceholdersHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contest_description_with_placeholders_new_with_title")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string objectId,
                [MarshalAs(UnmanagedType.LPStr)] string electoralDistrictId,
                ulong sequenceOrder,
                VoteVariationType voteVariation,
                ulong numberElected,
                ulong votesAllowed,
                [MarshalAs(UnmanagedType.LPStr)] string name,
                InternationalizedText.InternationalizedTextHandle ballotTitle,
                InternationalizedText.InternationalizedTextHandle ballotSubTitle,
                // TODO ISSUE #212: type safety
                [MarshalAs(UnmanagedType.LPArray)] IntPtr[] selections,
                ulong selectionsSize,
                // TODO ISSUE #212: type safety
                [MarshalAs(UnmanagedType.LPArray)] IntPtr[] placeholders,
                ulong placeholdersSize,
                out ContestDescriptionWithPlaceholdersHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contest_description_with_placeholders_new_with_parties")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string objectId,
                [MarshalAs(UnmanagedType.LPStr)] string electoralDistrictId,
                ulong sequenceOrder,
                VoteVariationType voteVariation,
                ulong numberElected,
                [MarshalAs(UnmanagedType.LPStr)] string name,
                // TODO ISSUE #212: type safety
                [MarshalAs(UnmanagedType.LPArray)] IntPtr[] selections,
                ulong selectionsSize,
                // TODO ISSUE #212: type safety
                [MarshalAs(UnmanagedType.LPArray)] string[] primaryPartyIds,
                ulong primaryPartyIdsSize,
                // TODO ISSUE #212: type safety
                [MarshalAs(UnmanagedType.LPArray)] IntPtr[] placeholders,
                ulong placeholdersSize,
                out ContestDescriptionWithPlaceholdersHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contest_description_with_placeholders_new_with_title_and_parties")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string objectId,
                [MarshalAs(UnmanagedType.LPStr)] string electoralDistrictId,
                ulong sequenceOrder,
                VoteVariationType voteVariation,
                ulong numberElected,
                ulong votesAllowed,
                [MarshalAs(UnmanagedType.LPStr)] string name,
                InternationalizedText.InternationalizedTextHandle ballotTitle,
                InternationalizedText.InternationalizedTextHandle ballotSubTitle,
                // TODO ISSUE #212: type safety
                [MarshalAs(UnmanagedType.LPArray)] IntPtr[] selections,
                ulong selectionsSize,
                // TODO ISSUE #212: type safety
                [MarshalAs(UnmanagedType.LPArray)] string[] primaryPartyIds,
                ulong primaryPartyIdsSize,
                // TODO ISSUE #212: type safety
                [MarshalAs(UnmanagedType.LPArray)] IntPtr[] placeholders,
                ulong placeholdersSize,
                out ContestDescriptionWithPlaceholdersHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contest_description_with_placeholders_free")]
            internal static extern Status Free(ContestDescriptionWithPlaceholdersHandle handle);

            #region ContestDescription Methods

            // Since the underlying c++ class inherits from ContestDescription
            // these functions call those methods subsituting the 
            // ContestDescriptionWithPlaceholdersHandle opaque pointer type

            [DllImport(DllName, EntryPoint = "eg_contest_description_get_object_id")]
            internal static extern Status GetObjectId(
                ContestDescriptionWithPlaceholdersHandle handle, out IntPtr objectId);

            [DllImport(DllName, EntryPoint = "eg_contest_description_get_electoral_district_id")]
            internal static extern Status GetElectoralDistrictId(
                ContestDescriptionWithPlaceholdersHandle handle, out IntPtr electoralDistrictId);

            [DllImport(DllName, EntryPoint = "eg_contest_description_get_sequence_order")]
            internal static extern ulong GetSequenceOrder(
                ContestDescriptionWithPlaceholdersHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contest_description_get_vote_variation")]
            internal static extern VoteVariationType GetVoteVariationType(
                ContestDescriptionWithPlaceholdersHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contest_description_get_number_elected")]
            internal static extern ulong GetNumberElected(
                ContestDescriptionWithPlaceholdersHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contest_description_get_votes_allowed")]
            internal static extern ulong GetVotesAllowed(
                ContestDescriptionWithPlaceholdersHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contest_description_get_name")]
            internal static extern Status GetName(
                ContestDescriptionWithPlaceholdersHandle handle, out IntPtr name);

            [DllImport(DllName, EntryPoint = "eg_contest_description_get_ballot_title")]
            internal static extern Status GetBallotTitle(
                ContestDescriptionWithPlaceholdersHandle handle, out InternationalizedText.InternationalizedTextHandle name);

            [DllImport(DllName, EntryPoint = "eg_contest_description_get_ballot_subtitle")]
            internal static extern Status GetBallotSubTitle(
                ContestDescriptionWithPlaceholdersHandle handle, out InternationalizedText.InternationalizedTextHandle name);

            [DllImport(DllName, EntryPoint = "eg_contest_description_get_selections_size")]
            internal static extern ulong GetSelectionsSize(
                ContestDescriptionWithPlaceholdersHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contest_description_get_selection_at_index")]
            internal static extern Status GetSelectionAtIndex(
                ContestDescriptionWithPlaceholdersHandle handle,
                ulong index,
                out SelectionDescription.SelectionDescriptionHandle partyId);

            [DllImport(DllName, EntryPoint = "eg_contest_description_crypto_hash")]
            internal static extern Status CryptoHash(
                ContestDescriptionWithPlaceholdersHandle handle,
                out ElementModQ.ElementModQHandle crypto_hash);

            #endregion

            [DllImport(DllName, EntryPoint = "eg_contest_description_with_placeholders_get_placeholders_size")]
            internal static extern ulong GetPlaceholdersSize(
                ContestDescriptionWithPlaceholdersHandle handle);

            [DllImport(DllName, EntryPoint = "eg_contest_description_with_placeholders_get_placeholder_at_index")]
            internal static extern Status GetPlaceholderAtIndex(
                ContestDescriptionWithPlaceholdersHandle handle,
                ulong index,
                out SelectionDescription.SelectionDescriptionHandle partyId);

            [DllImport(DllName, EntryPoint = "eg_contest_description_with_placeholders_is_placeholder")]
            internal static extern bool IsPlaceholder(
                ContestDescriptionWithPlaceholdersHandle handle, SelectionDescription.SelectionDescriptionHandle selection);

            [DllImport(DllName, EntryPoint = "eg_contest_description_with_placeholders_selection_for_id")]
            internal static extern Status SelectionForId(
                ContestDescriptionWithPlaceholdersHandle handle,
                [MarshalAs(UnmanagedType.LPStr)] string selectionId,
                SelectionDescription.SelectionDescriptionHandle selection);
        }

        #endregion

        #region Manifest

        internal static unsafe class Manifest
        {
            internal unsafe struct ManifestType { };

            internal class ManifestHandle
                : ElectionguardSafeHandle<ManifestType>
            {
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = Manifest.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"Manifest Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_election_manifest_new")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string electionScopeId,
                ElectionType electionType,
                ulong startDate,
                ulong endDate,
                // TODO ISSUE #212: type safety
                [MarshalAs(UnmanagedType.LPArray)] IntPtr[] gpUnits,
                ulong gpUnitsSize,
                // TODO ISSUE #212: type safety
                [MarshalAs(UnmanagedType.LPArray)] IntPtr[] parties,
                ulong partiesSize,
                // TODO ISSUE #212: type safety
                [MarshalAs(UnmanagedType.LPArray)] IntPtr[] candidates,
                ulong candidatesSize,
                // TODO ISSUE #212: type safety
                [MarshalAs(UnmanagedType.LPArray)] IntPtr[] contests,
                ulong contestSize,
                // TODO ISSUE #212: type safety
                [MarshalAs(UnmanagedType.LPArray)] IntPtr[] ballotStyles,
                ulong ballotStylesSize,
                out ManifestHandle handle);

            // TODO: eg_election_manifest_new_with_contact

            [DllImport(DllName, EntryPoint = "eg_election_manifest_free")]
            internal static extern Status Free(ManifestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_get_election_scope_id")]
            internal static extern Status GetElectionScopeId(
                ManifestHandle handle, out IntPtr election_scope_id);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_get_type")]
            internal static extern ElectionType GetElectionType(
                ManifestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_get_start_date")]
            internal static extern ulong GetStartDate(
                ManifestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_get_end_date")]
            internal static extern ulong GetEndDate(
                ManifestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_get_geopolitical_units_size")]
            internal static extern ulong GetGeopoliticalUnitsSize(
                ManifestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_get_geopolitical_unit_at_index")]
            internal static extern Status GetGeopoliticalUnitAtIndex(
                ManifestHandle handle,
                ulong index,
                out GeopoliticalUnit.GeopoliticalUnitHandle gpUnit);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_get_parties_size")]
            internal static extern ulong GetPartiesSize(
                ManifestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_get_party_at_index")]
            internal static extern Status GetPartyAtIndex(
                ManifestHandle handle,
                ulong index,
                out Party.PartyHandle party);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_get_candidates_size")]
            internal static extern ulong GetCandidatesSize(
                ManifestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_get_candidate_at_index")]
            internal static extern Status GetCandidateAtIndex(
                ManifestHandle handle,
                ulong index,
                out Candidate.CandidateHandle candidate);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_get_contests_size")]
            internal static extern ulong GetContestsSize(
                ManifestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_get_contest_at_index")]
            internal static extern Status GetContestAtIndex(
                ManifestHandle handle,
                ulong index,
                out ContestDescription.ContestDescriptionHandle contest);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_get_ballot_styles_size")]
            internal static extern ulong GetBallotStylesSize(
                ManifestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_get_ballot_style_at_index")]
            internal static extern Status GetBallotStyleAtIndex(
                ManifestHandle handle,
                ulong index,
                out BallotStyle.BallotStyleHandle ballotStyle);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_get_name")]
            internal static extern Status GetName(
                ManifestHandle handle,
                out InternationalizedText.InternationalizedTextHandle name);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_get_contact_info")]
            internal static extern Status GetContactInfo(
                ManifestHandle handle,
                out ContactInformation.ContactInformationHandle contactInfo);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_crypto_hash")]
            internal static extern Status CryptoHash(
                ManifestHandle handle,
                out ElementModQ.ElementModQHandle crypto_hash);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_is_valid")]
            internal static extern bool IsValid(ManifestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_from_json")]
            internal static extern Status FromJson(
                [MarshalAs(UnmanagedType.LPStr)] string data,
                out ManifestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_from_bson")]
            internal static extern Status FromBson(
                byte* data, ulong length, out ManifestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_from_msgpack")]
            internal static extern Status FromMsgPack(
                byte* data, ulong length, out ManifestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_to_json")]
            internal static extern Status ToJson(
                ManifestHandle handle, out IntPtr data, out UIntPtr size);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_to_bson")]
            internal static extern Status ToBson(
                ManifestHandle handle, out IntPtr data, out UIntPtr size);

            [DllImport(DllName, EntryPoint = "eg_election_manifest_to_msgpack")]
            internal static extern Status ToMsgPack(
                ManifestHandle handle, out IntPtr data, out UIntPtr size);
        }

        #endregion

        #region InternalManifest

        internal static unsafe class InternalManifest
        {
            internal unsafe struct InternalManifestType { };

            internal class InternalManifestHandle
                : ElectionguardSafeHandle<InternalManifestType>
            {
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = InternalManifest.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"InternalManifest Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_internal_manifest_new")]
            internal static extern Status New(
                Manifest.ManifestHandle manifest,
                out InternalManifestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_internal_manifest_free")]
            internal static extern Status Free(
                InternalManifestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_internal_manifest_get_manifest_hash")]
            internal static extern Status GetManifestHash(
                InternalManifestHandle handle,
                out ElementModQ.ElementModQHandle manifest_hash);

            [DllImport(DllName, EntryPoint = "eg_internal_manifest_get_geopolitical_units_size")]
            internal static extern ulong GetGeopoliticalUnitsSize(
                InternalManifestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_internal_manifest_get_geopolitical_unit_at_index")]
            internal static extern Status GetGeopoliticalUnitAtIndex(
                InternalManifestHandle handle,
                ulong index,
                out GeopoliticalUnit.GeopoliticalUnitHandle gpUnit);

            [DllImport(DllName, EntryPoint = "eg_internal_manifest_get_contests_size")]
            internal static extern ulong GetContestsSize(
                InternalManifestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_internal_manifest_get_contest_at_index")]
            internal static extern Status GetContestAtIndex(
                InternalManifestHandle handle,
                ulong index,
                out ContestDescription.ContestDescriptionHandle contest);

            [DllImport(DllName, EntryPoint = "eg_internal_manifest_get_ballot_styles_size")]
            internal static extern ulong GetBallotStylesSize(
                InternalManifestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_internal_manifest_get_ballot_style_at_index")]
            internal static extern Status GetBallotStyleAtIndex(
                InternalManifestHandle handle,
                ulong index,
                out BallotStyle.BallotStyleHandle ballotStyle);

            [DllImport(DllName, EntryPoint = "eg_internal_manifest_from_json")]
            internal static extern Status FromJson(
                [MarshalAs(UnmanagedType.LPStr)] string data,
                out InternalManifestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_internal_manifest_from_bson")]
            internal static extern Status FromBson(
                byte* data, ulong length, out InternalManifestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_internal_manifest_from_msgpack")]
            internal static extern Status FromMsgPack(
                byte* data, ulong length, out InternalManifestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_internal_manifest_to_json")]
            internal static extern Status ToJson(
                InternalManifestHandle handle, out IntPtr data, out UIntPtr size);

            [DllImport(DllName, EntryPoint = "eg_internal_manifest_to_bson")]
            internal static extern Status ToBson(
                InternalManifestHandle handle, out IntPtr data, out UIntPtr size);

            [DllImport(DllName, EntryPoint = "eg_internal_manifest_to_msgpack")]
            internal static extern Status ToMsgPack(
                InternalManifestHandle handle, out IntPtr data, out UIntPtr size);
        }

        #endregion

        #region CiphertextElectionContext

        internal static unsafe class CiphertextElectionContext
        {
            internal unsafe struct CiphertextElectionType { };

            internal class CiphertextElectionContextHandle
                : ElectionguardSafeHandle<CiphertextElectionType>
            {
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = CiphertextElectionContext.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"CiphertextElectionContext Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_ciphertext_election_context_free")]
            internal static extern Status Free(CiphertextElectionContextHandle handle);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_election_context_get_elgamal_public_key")]
            internal static extern Status GetElGamalPublicKey(
                CiphertextElectionContextHandle handle,
                out ElementModP.ElementModPHandle elgamal_public_key);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_election_context_get_commitment_hash")]
            internal static extern Status GetCommitmentHash(
                CiphertextElectionContextHandle handle,
                out ElementModQ.ElementModQHandle commitment_hash);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_election_context_get_manifest_hash")]
            internal static extern Status GetManifestHash(
                CiphertextElectionContextHandle handle,
                out ElementModQ.ElementModQHandle manifest_hash);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_election_context_get_crypto_base_hash")]
            internal static extern Status GetCryptoBaseHash(
                CiphertextElectionContextHandle handle,
                out ElementModQ.ElementModQHandle crypto_base_hash);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_election_context_get_crypto_extended_base_hash")]
            internal static extern Status GetCryptoExtendedBaseHash(
                CiphertextElectionContextHandle handle,
                out ElementModQ.ElementModQHandle crypto_extended_base_hash);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_election_context_make")]
            internal static extern Status Make(
                ulong number_of_guardians,
                ulong quorum,
                ElementModP.ElementModPHandle elgamal_public_key,
                ElementModQ.ElementModQHandle commitment_hash,
                ElementModQ.ElementModQHandle manifest_hash,
                out CiphertextElectionContextHandle handle);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_election_context_make_from_hex")]
            internal static extern Status Make(
                ulong number_of_guardians,
                ulong quorum,
                [MarshalAs(UnmanagedType.LPStr)] string hex_elgamal_public_key,
                [MarshalAs(UnmanagedType.LPStr)] string hex_commitment_hash,
                [MarshalAs(UnmanagedType.LPStr)] string hex_manifest_hash,
                out CiphertextElectionContextHandle handle);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_election_context_from_json")]
            internal static extern Status FromJson(
                [MarshalAs(UnmanagedType.LPStr)] string data,
                out CiphertextElectionContextHandle handle);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_election_context_from_bson")]
            internal static extern Status FromBson(
                uint* data, ulong length, CiphertextElectionContextHandle handle);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_election_context_to_json")]
            internal static extern Status ToJson(
                CiphertextElectionContextHandle handle, out IntPtr data, out UIntPtr size);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_election_context_to_bson")]
            internal static extern Status ToBson(
                CiphertextElectionContextHandle handle, out uint* data, out UIntPtr size);
        }

        #endregion

        #region ExtendedData

        internal static unsafe class ExtendedData
        {
            internal unsafe struct ExtendedDataType { };

            internal class ExtendedDataHandle
                : ElectionguardSafeHandle<ExtendedDataType>
            {
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = ExtendedData.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"ExtendedData Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_extended_data_new")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string value,
                long length,
                out ExtendedDataHandle handle);

            [DllImport(DllName, EntryPoint = "eg_extended_data_free")]
            internal static extern Status Free(ExtendedDataHandle handle);

            [DllImport(DllName, EntryPoint = "eg_extended_data_get_value")]
            internal static extern Status GetValue(
                ExtendedDataHandle handle, out IntPtr object_id);

            [DllImport(DllName, EntryPoint = "eg_extended_data_get_length")]
            internal static extern long GetLength(ExtendedDataHandle handle);

        }

        #endregion

        #region Ballot

        internal static unsafe class PlaintextBallotSelection
        {
            internal unsafe struct PlaintextBallotSelectionType { };

            internal class PlaintextBallotSelectionHandle
                : ElectionguardSafeHandle<PlaintextBallotSelectionType>
            {
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = PlaintextBallotSelection.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"PlaintextBallotSelection Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_selection_new")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string objectId,
                long vote,
                bool isPlaceholderSelection,
                out PlaintextBallotSelectionHandle handle);

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_selection_new_with_extended_data")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string objectId,
                long vote,
                bool isPlaceholderSelection,
                [MarshalAs(UnmanagedType.LPStr)] string extendedData,
                long extendedDataLength,
                out PlaintextBallotSelectionHandle handle);

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_selection_free")]
            internal static extern Status Free(PlaintextBallotSelectionHandle handle);

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_selection_get_object_id")]
            internal static extern Status GetObjectId(
                PlaintextBallotSelectionHandle handle, out IntPtr object_id);

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_selection_get_vote")]
            internal static extern long GetVote(
                PlaintextBallotSelectionHandle handle);

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_selection_get_is_placeholder")]
            internal static extern bool GetIsPlaceholder(
                PlaintextBallotSelectionHandle handle);

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_selection_get_extended_data")]
            internal static extern Status GetExtendedData(
                PlaintextBallotSelectionHandle handle, out ExtendedData.ExtendedDataHandle extended_data);

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_selection_is_valid")]
            internal static extern bool IsValid(
                PlaintextBallotSelectionHandle handle,
                [MarshalAs(UnmanagedType.LPStr)] string expectedObjectId);
        }

        internal static unsafe class CiphertextBallotSelection
        {
            internal unsafe struct CiphertextBallotSelectionType { };

            internal class CiphertextBallotSelectionHandle
                : ElectionguardSafeHandle<CiphertextBallotSelectionType>
            {
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = CiphertextBallotSelection.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"CiphertextBallotSelection Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_selection_free")]
            internal static extern Status Free(CiphertextBallotSelectionHandle handle);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_selection_get_object_id")]
            internal static extern Status GetObjectId(
                CiphertextBallotSelectionHandle handle, out IntPtr object_id);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_selection_get_sequence_order")]
            internal static extern long GetSequenceOrder(CiphertextBallotSelectionHandle handle);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_selection_get_description_hash")]
            internal static extern Status GetDescriptionHash(
                CiphertextBallotSelectionHandle handle,
                out ElementModQ.ElementModQHandle description_hash);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_selection_get_is_placeholder")]
            internal static extern bool GetIsPlaceholder(CiphertextBallotSelectionHandle handle);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_selection_get_ciphertext")]
            internal static extern Status GetCiphertext(
                CiphertextBallotSelectionHandle handle,
                out ElGamalCiphertext.ElGamalCiphertextHandle ciphertext);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_selection_get_crypto_hash")]
            internal static extern Status GetCryptoHash(
                CiphertextBallotSelectionHandle handle,
                out ElementModQ.ElementModQHandle cryptoHash);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_selection_get_nonce")]
            internal static extern Status GetNonce(
                CiphertextBallotSelectionHandle handle,
                out ElementModQ.ElementModQHandle nonce);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_selection_get_proof")]
            internal static extern Status GetProof(
                CiphertextBallotSelectionHandle handle,
                out DisjunctiveChaumPedersenProof.DisjunctiveChaumPedersenProofHandle nonce);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_selection_crypto_hash_with")]
            internal static extern Status CryptoHashWith(
                CiphertextBallotSelectionHandle handle,
                ElementModQ.ElementModQHandle encryption_seed,
                out ElementModQ.ElementModQHandle crypto_hash);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_selection_is_valid_encryption")]
            internal static extern bool IsValidEncryption(
                CiphertextBallotSelectionHandle handle,
                ElementModQ.ElementModQHandle encryption_seed,
                ElementModP.ElementModPHandle public_key,
                ElementModQ.ElementModQHandle crypto_extended_base_hash);

        }

        internal static unsafe class PlaintextBallotContest
        {
            internal unsafe struct PlaintextBallotContestType { };

            internal class PlaintextBallotContestHandle
                : ElectionguardSafeHandle<PlaintextBallotContestType>
            {
                [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = PlaintextBallotContest.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"PlaintextBallotContest Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_contest_new")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string objectId,
                // TODO: type safety
                [MarshalAs(UnmanagedType.LPArray)] IntPtr[] selections,
                ulong selectionsSize,
                out PlaintextBallotContestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_contest_free")]
            internal static extern Status Free(PlaintextBallotContestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_contest_get_object_id")]
            internal static extern Status GetObjectId(
                PlaintextBallotContestHandle handle, out IntPtr object_id);

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_contest_get_selections_size")]
            internal static extern ulong GetSelectionsSize(
                PlaintextBallotContestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_contest_get_selection_at_index")]
            internal static extern Status GetSelectionAtIndex(
                PlaintextBallotContestHandle handle,
                ulong index,
                out PlaintextBallotSelection.PlaintextBallotSelectionHandle selection);

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_contest_is_valid")]
            internal static extern bool IsValid(
                PlaintextBallotContestHandle handle,
                [MarshalAs(UnmanagedType.LPStr)] string expected_object_id,
                long expected_num_selections,
                long expected_num_elected,
                long votes_allowed);
        }

        internal static unsafe class CiphertextBallotContest
        {
            internal unsafe struct CiphertextBallotContestType { };

            internal class CiphertextBallotContestHandle
                : ElectionguardSafeHandle<CiphertextBallotContestType>
            {
                [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = CiphertextBallotContest.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"CiphertextBallotContest Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_contest_free")]
            internal static extern Status Free(CiphertextBallotContestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_contest_get_object_id")]
            internal static extern Status GetObjectId(
                CiphertextBallotContestHandle handle, out IntPtr object_id);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_contest_get_sequence_order")]
            internal static extern long GetSequenceOrder(CiphertextBallotContestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_contest_get_description_hash")]
            internal static extern Status GetDescriptionHash(
                CiphertextBallotContestHandle handle,
                out ElementModQ.ElementModQHandle description_hash);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_contest_get_selections_size")]
            internal static extern ulong GetSelectionsSize(
                CiphertextBallotContestHandle handle);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_contest_get_selection_at_index")]
            internal static extern Status GetSelectionAtIndex(
                CiphertextBallotContestHandle handle,
                ulong index,
                out CiphertextBallotSelection.CiphertextBallotSelectionHandle selection);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_contest_get_nonce")]
            internal static extern Status GetNonce(
                CiphertextBallotContestHandle handle,
                out ElementModQ.ElementModQHandle nonce);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_contest_get_ciphertext_accumulation")]
            internal static extern Status GetCiphertextAccumulation(
                CiphertextBallotContestHandle handle,
                out ElGamalCiphertext.ElGamalCiphertextHandle nonce);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_contest_get_crypto_hash")]
            internal static extern Status GetCryptoHash(
                CiphertextBallotContestHandle handle,
                out ElementModQ.ElementModQHandle cryptoHash);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_contest_get_proof")]
            internal static extern Status GetProof(
                CiphertextBallotContestHandle handle,
                out ConstantChaumPedersenProof.ConstantChaumPedersenProofHandle nonce);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_contest_crypto_hash_with")]
            internal static extern Status CryptoHashWith(
                CiphertextBallotContestHandle handle,
                ElementModQ.ElementModQHandle encryption_seed,
                out ElementModQ.ElementModQHandle crypto_hash);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_contest_aggregate_nonce")]
            internal static extern Status AggregateNonce(
                CiphertextBallotContestHandle handle,
                out ElementModQ.ElementModQHandle aggregate_nonce);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_contest_elgamal_accumulate")]
            internal static extern Status ElGamalAccumulate(
                CiphertextBallotContestHandle handle,
                out ElGamalCiphertext.ElGamalCiphertextHandle ciphertext_accumulation);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_selection_is_valid_encryption")]
            internal static extern bool IsValidEncryption(
                CiphertextBallotContestHandle handle,
                ElementModQ.ElementModQHandle encryption_seed,
                ElementModP.ElementModPHandle public_key,
                ElementModQ.ElementModQHandle crypto_extended_base_hash);
        }

        internal static unsafe class PlaintextBallot
        {
            internal unsafe struct PlaintextBallotType { };

            internal class PlaintextBallotHandle
                : ElectionguardSafeHandle<PlaintextBallotType>
            {
                [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = PlaintextBallot.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"PlaintextBallot Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_new")]
            internal static extern Status New(
                [MarshalAs(UnmanagedType.LPStr)] string objectId,
                [MarshalAs(UnmanagedType.LPStr)] string styleId,
                // TODO: type safety
                [MarshalAs(UnmanagedType.LPArray)] IntPtr[] contests,
                ulong contestsSize,
                out PlaintextBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_free")]
            internal static extern Status Free(PlaintextBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_get_object_id")]
            internal static extern Status GetObjectId(
                PlaintextBallotHandle handle, out IntPtr object_id);

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_get_style_id")]
            internal static extern Status GetStyleId(
                PlaintextBallotHandle handle, out IntPtr style_id);

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_get_contests_size")]
            internal static extern ulong GetContestsSize(PlaintextBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_get_contest_at_index")]
            internal static extern Status GetContestAtIndex(
                PlaintextBallotHandle handle,
                ulong index,
                out PlaintextBallotContest.PlaintextBallotContestHandle contest);

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_from_json")]
            internal static extern Status FromJson(
                [MarshalAs(UnmanagedType.LPStr)] string data,
                out PlaintextBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_from_bson")]
            internal static extern Status FromBson(
                byte* data, ulong length, out PlaintextBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_from_msgpack")]
            internal static extern Status FromMsgPack(
                byte* data, ulong length, out PlaintextBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_to_json")]
            internal static extern Status ToJson(
                PlaintextBallotHandle handle, out IntPtr data, out UIntPtr size);

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_to_bson")]
            internal static extern Status ToBson(
                PlaintextBallotHandle handle, out IntPtr data, out UIntPtr size);

            [DllImport(DllName, EntryPoint = "eg_plaintext_ballot_to_msgpack")]
            internal static extern Status ToMsgPack(
                PlaintextBallotHandle handle, out IntPtr data, out UIntPtr size);
        }

        internal static unsafe class CompactPlaintextBallot
        {
            internal unsafe struct CompactPlaintextBallotType { };

            internal class CompactPlaintextBallotHandle
                : ElectionguardSafeHandle<CompactPlaintextBallotType>
            {
                [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = CompactPlaintextBallot.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"CompactPlaintextBallot Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_compact_plaintext_ballot_free")]
            internal static extern Status Free(CompactPlaintextBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_compact_plaintext_ballot_from_msgpack")]
            internal static extern Status FromMsgPack(
                byte* data, ulong size, out CompactPlaintextBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_compact_plaintext_ballot_to_msgpack")]
            internal static extern Status ToMsgPack(
                CompactPlaintextBallotHandle handle, out IntPtr data, out UIntPtr size);

            [DllImport(DllName, EntryPoint = "eg_compact_plaintext_ballot_msgpack_free")]
            internal static extern Status MsgPackFree(IntPtr data);
        }

        internal static unsafe class CiphertextBallot
        {
            internal unsafe struct CiphertextBallotType { };

            internal class CiphertextBallotHandle
                : ElectionguardSafeHandle<CiphertextBallotType>
            {
                [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = CiphertextBallot.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"CiphertextBallot Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_free")]
            internal static extern Status Free(CiphertextBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_get_object_id")]
            internal static extern Status GetObjectId(
                CiphertextBallotHandle handle, out IntPtr object_id);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_get_style_id")]
            internal static extern Status GetStyleId(
                CiphertextBallotHandle handle, out IntPtr style_id);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_get_manifest_hash")]
            internal static extern Status GetManifestHash(
                CiphertextBallotHandle handle,
                out ElementModQ.ElementModQHandle manifest_hash_ref);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_get_ballot_code_seed")]
            internal static extern Status GetBallotCodeSeed(
                CiphertextBallotHandle handle,
                out ElementModQ.ElementModQHandle ballot_code_seed_ref);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_get_contests_size")]
            internal static extern ulong GetContestsSize(CiphertextBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_get_contest_at_index")]
            internal static extern Status GetContestAtIndex(
                CiphertextBallotHandle handle,
                ulong index,
                out CiphertextBallotContest.CiphertextBallotContestHandle contest_ref);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_get_ballot_code")]
            internal static extern Status GetBallotCode(
                CiphertextBallotHandle handle,
                out ElementModQ.ElementModQHandle ballot_code_ref);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_get_timestamp")]
            internal static extern Status GetTimestamp(
                CiphertextBallotHandle handle,
                out long timestamp);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_get_nonce")]
            internal static extern Status GetNonce(
                CiphertextBallotHandle handle,
                out ElementModQ.ElementModQHandle nonce_ref);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_get_crypto_hash")]
            internal static extern Status GetCryptoHash(
                CiphertextBallotHandle handle,
                out ElementModQ.ElementModQHandle hash_ref);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_crypto_hash_with")]
            internal static extern Status CryptoHashWith(
                CiphertextBallotHandle handle,
                ElementModQ.ElementModQHandle manifest_hash,
                out ElementModQ.ElementModQHandle crypto_hash);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_is_valid_encryption")]
            internal static extern bool IsValidEncryption(
                CiphertextBallotHandle handle,
                ElementModQ.ElementModQHandle manifest_hash,
                ElementModP.ElementModPHandle public_key,
                ElementModQ.ElementModQHandle crypto_extended_base_hash);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_from_json")]
            internal static extern Status FromJson(
                [MarshalAs(UnmanagedType.LPStr)] string data,
                out CiphertextBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_from_bson")]
            internal static extern Status FromBson(
                byte* data, ulong length, out CiphertextBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_from_msgpack")]
            internal static extern Status FromMsgPack(
                byte* data, ulong length, out CiphertextBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_to_json")]
            internal static extern Status ToJson(
                CiphertextBallotHandle handle, out IntPtr data, out UIntPtr size);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_to_json_with_nonces")]
            internal static extern Status ToJsonWithNonces(
                CiphertextBallotHandle handle, out IntPtr data, out UIntPtr size);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_to_bson")]
            internal static extern Status ToBson(
                CiphertextBallotHandle handle, out IntPtr data, out UIntPtr size);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_to_bson_with_nonces")]
            internal static extern Status ToBsonWithNonces(
                CiphertextBallotHandle handle, out IntPtr data, out UIntPtr size);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_to_msgpack")]
            internal static extern Status ToMsgPack(
                CiphertextBallotHandle handle, out IntPtr data, out UIntPtr size);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_to_msgpack_with_nonces")]
            internal static extern Status ToMsgPackWithNonces(
                CiphertextBallotHandle handle, out IntPtr data, out UIntPtr size);
        }

        internal static unsafe class CompactCiphertextBallot
        {
            internal unsafe struct CompactCiphertextBallotType { };

            internal class CompactCiphertextBallotHandle
                : ElectionguardSafeHandle<CompactCiphertextBallotType>
            {
                [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = CompactCiphertextBallot.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"CompactCiphertextBallot Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_compact_ciphertext_ballot_free")]
            internal static extern Status Free(CompactCiphertextBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_compact_ciphertext_ballot_get_object_id")]
            internal static extern Status GetObjectId(
                CompactCiphertextBallotHandle handle, out IntPtr object_id);

            [DllImport(DllName, EntryPoint = "eg_compact_ciphertext_ballot_from_msgpack")]
            internal static extern Status FromMsgPack(
                byte* data, ulong size, out CompactCiphertextBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_compact_ciphertext_ballot_to_msgpack")]
            internal static extern Status ToMsgPack(
                CompactCiphertextBallotHandle handle, out IntPtr data, out UIntPtr size);

            [DllImport(DllName, EntryPoint = "eg_compact_ciphertext_ballot_msgpack_free")]
            internal static extern Status MsgPackFree(IntPtr data);

        }

        #endregion

        #region SubmittedBallot

        internal static unsafe class SubmittedBallot
        {
            internal unsafe struct SubmittedBallotType { };

            internal class SubmittedBallotHandle
                : ElectionguardSafeHandle<SubmittedBallotType>
            {
                [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = SubmittedBallot.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"SubmittedBallot Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_submitted_ballot_free")]
            internal static extern Status Free(SubmittedBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_submitted_ballot_get_state")]
            internal static extern BallotBoxState GetState(SubmittedBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_submitted_ballot_from")]
            internal static extern Status From(
                CiphertextBallot.CiphertextBallotHandle ciphertext,
                BallotBoxState state,
                out SubmittedBallotHandle handle);

            #region CiphertextBallot Methods

            // Since the underlying c++ class inherits from CiphertextBallot
            // these functions call those methods subsituting the SubmittedBallot opaque pointer type

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_get_object_id")]
            internal static extern Status GetObjectId(
                SubmittedBallotHandle handle, out IntPtr object_id);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_get_style_id")]
            internal static extern Status GetStyleId(
               SubmittedBallotHandle handle, out IntPtr style_id);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_get_manifest_hash")]
            internal static extern Status GetManifestHash(
                SubmittedBallotHandle handle,
                out ElementModQ.ElementModQHandle manifest_hash_ref);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_get_ballot_code_seed")]
            internal static extern Status GetBallotCodeSeed(
                SubmittedBallotHandle handle,
                out ElementModQ.ElementModQHandle ballot_code_seed_ref);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_get_contests_size")]
            internal static extern ulong GetContestsSize(SubmittedBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_get_contest_at_index")]
            internal static extern Status GetContestAtIndex(
                SubmittedBallotHandle handle,
                ulong index,
                out CiphertextBallotContest.CiphertextBallotContestHandle contest_ref);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_get_ballot_code")]
            internal static extern Status GetBallotCode(
                SubmittedBallotHandle handle,
                out ElementModQ.ElementModQHandle ballot_code_ref);

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_get_timestamp")]
            internal static extern Status GetTimestamp(
                SubmittedBallotHandle handle,
                out long timestamp);

            // GetNonce is not provided

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_get_crypto_hash")]
            internal static extern Status GetCryptoHash(
                SubmittedBallotHandle handle,
                out ElementModQ.ElementModQHandle hash_ref);

            // CryptoHashWith is not provided

            [DllImport(DllName, EntryPoint = "eg_ciphertext_ballot_is_valid_encryption")]
            internal static extern bool IsValidEncryption(
                SubmittedBallotHandle handle,
                ElementModQ.ElementModQHandle manifest_hash,
                ElementModP.ElementModPHandle public_key,
                ElementModQ.ElementModQHandle crypto_extended_base_hash);

            #endregion

            [DllImport(DllName, EntryPoint = "eg_submitted_ballot_from_json")]
            internal static extern Status FromJson(
                [MarshalAs(UnmanagedType.LPStr)] string data,
                out SubmittedBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_submitted_ballot_from_bson")]
            internal static extern Status FromBson(
                byte* data, ulong length, out SubmittedBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_submitted_ballot_from_msgpack")]
            internal static extern Status FromMsgPack(
                byte* data, ulong length, out SubmittedBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_submitted_ballot_to_json")]
            internal static extern Status ToJson(
                SubmittedBallotHandle handle, out IntPtr data, out UIntPtr size);

            [DllImport(DllName, EntryPoint = "eg_submitted_ballot_to_bson")]
            internal static extern Status ToBson(
                SubmittedBallotHandle handle, out IntPtr data, out UIntPtr size);

            [DllImport(DllName, EntryPoint = "eg_submitted_ballot_to_msgpack")]
            internal static extern Status ToMsgPack(
                SubmittedBallotHandle handle, out IntPtr data, out UIntPtr size);

        }

        #endregion

        #region Encrypt

        internal static unsafe class EncryptionDevice
        {
            internal unsafe struct EncryptionDeviceType { };

            internal class EncryptionDeviceHandle
                : ElectionguardSafeHandle<EncryptionDeviceType>
            {
                [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = EncryptionDevice.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"EncryptionDevice Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_encryption_device_new")]
            internal static extern Status New(
                ulong deviceUuid,
                ulong sessionUuid,
                ulong launchCode,
                [MarshalAs(UnmanagedType.LPStr)] string location,
                out EncryptionDeviceHandle handle);

            [DllImport(DllName, EntryPoint = "eg_encryption_device_free")]
            internal static extern Status Free(EncryptionDeviceHandle handle);

            [DllImport(DllName, EntryPoint = "eg_encryption_device_get_hash")]
            internal static extern Status GetHash(
                EncryptionDeviceHandle handle,
                out ElementModQ.ElementModQHandle device_hash);
        }

        internal static unsafe class EncryptionMediator
        {
            internal unsafe struct EncryptionMediatorType { };

            internal class EncryptionMediatorHandle
                : ElectionguardSafeHandle<EncryptionMediatorType>
            {
                [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
                protected override bool Free()
                {
                    if (IsClosed) return true;

                    var status = EncryptionMediator.Free(this);
                    if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                    {
                        Console.WriteLine($"EncryptionMediator Error Free: {status}");
                        return false;
                    }
                    return true;
                }
            }

            [DllImport(DllName, EntryPoint = "eg_encryption_mediator_new")]
            internal static extern Status New(
                InternalManifest.InternalManifestHandle manifest,
                CiphertextElectionContext.CiphertextElectionContextHandle context,
                EncryptionDevice.EncryptionDeviceHandle device,
                out EncryptionMediatorHandle handle);

            [DllImport(DllName, EntryPoint = "eg_encryption_mediator_free")]
            internal static extern Status Free(EncryptionMediatorHandle handle);

            [DllImport(DllName, EntryPoint = "eg_encryption_mediator_compact_encrypt_ballot")]
            internal static extern Status CompactEncrypt(
                EncryptionMediatorHandle handle,
                PlaintextBallot.PlaintextBallotHandle plainutext,
                out CompactCiphertextBallot.CompactCiphertextBallotHandle ciphertext);

            [DllImport(DllName, EntryPoint = "eg_encryption_mediator_compact_encrypt_ballot_verify_proofs")]
            internal static extern Status CompactEncryptAndVerify(
                EncryptionMediatorHandle handle,
                PlaintextBallot.PlaintextBallotHandle plainutext,
                out CompactCiphertextBallot.CompactCiphertextBallotHandle ciphertext);

            [DllImport(DllName, EntryPoint = "eg_encryption_mediator_encrypt_ballot")]
            internal static extern Status Encrypt(
                EncryptionMediatorHandle handle,
                PlaintextBallot.PlaintextBallotHandle plainutext,
                out CiphertextBallot.CiphertextBallotHandle ciphertext);

            [DllImport(DllName, EntryPoint = "eg_encryption_mediator_encrypt_ballot_verify_proofs")]
            internal static extern Status EncryptAndVerify(
                EncryptionMediatorHandle handle,
                PlaintextBallot.PlaintextBallotHandle plainutext,
                out CiphertextBallot.CiphertextBallotHandle ciphertext);
        }


        internal static unsafe class Encrypt
        {
            // TODO: when the manifest is completely exposed
            // [DllImport(DllName, EntryPoint = "eg_encrypt_selection")]
            // internal static extern Status Selection(
            //     PlaintextBallotSelection.PlaintextBallotSelectionHandle plaintext,
            //     SelectionDescription.SelectionDescriptionHandle metadata,
            //     CiphertextElectionContext.CiphertextElectionContextHandle context,
            //     ElementModQ.ElementModQHandle ballot_code_seed,
            //     ElementModQ.ElementModQHandle nonce,
            //     bool shouldVerifyProofs,
            //     out CiphertextBallot.CiphertextBallotHandle handle);

            // [DllImport(DllName, EntryPoint = "eg_encrypt_contest")]
            // internal static extern Status Contest(
            //     PlaintextBallotContest.PlaintextBallotContestHandle plaintext,
            //     ContestDescription.InternalManifestHandle metadata,
            //     CiphertextElectionContext.CiphertextElectionContextHandle context,
            //     ElementModQ.ElementModQHandle ballot_code_seed,
            //     ElementModQ.ElementModQHandle nonce,
            //     bool shouldVerifyProofs,
            //     out CiphertextBallot.CiphertextBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_encrypt_ballot")]
            internal static extern Status Ballot(
                PlaintextBallot.PlaintextBallotHandle plaintext,
                InternalManifest.InternalManifestHandle internal_manifest,
                CiphertextElectionContext.CiphertextElectionContextHandle context,
                ElementModQ.ElementModQHandle ballot_code_seed,
                bool shouldVerifyProofs,
                out CiphertextBallot.CiphertextBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_encrypt_ballot_with_nonce")]
            internal static extern Status Ballot(
                PlaintextBallot.PlaintextBallotHandle plaintext,
                InternalManifest.InternalManifestHandle internal_manifest,
                CiphertextElectionContext.CiphertextElectionContextHandle context,
                ElementModQ.ElementModQHandle ballot_code_seed,
                ElementModQ.ElementModQHandle nonce,
                bool shouldVerifyProofs,
                out CiphertextBallot.CiphertextBallotHandle handle);

            [DllImport(DllName, EntryPoint = "eg_encrypt_compact_ballot")]
            internal static extern Status CompactBallot(
                PlaintextBallot.PlaintextBallotHandle plaintext,
                InternalManifest.InternalManifestHandle internal_manifest,
                CiphertextElectionContext.CiphertextElectionContextHandle context,
                ElementModQ.ElementModQHandle ballot_code_seed,
                bool shouldVerifyProofs,
                out CompactCiphertextBallot.CompactCiphertextBallotHandle handle);
        }
    }

    #endregion
}
