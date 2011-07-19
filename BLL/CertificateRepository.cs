using System;
using System.Collections.Generic;
using System.Linq;
using TSS.Models;

namespace TSS.BLL
{
    public class CertificateRepository : Repository<Certificate>
    {
        public IList<Certificate> GetAll(CertificateStatus status,string specialtyId=null) {
            return BuildQuery(status , specialtyId).ToList();
        }

        public IList<Certificate> GetAll(CertificateStatus status,int pageNo,int pageSize,out int rowCount,string specialtyId=null) {
            var query = BuildQuery(status , specialtyId);
            rowCount = query.Count();
            int skip = (pageNo - 1) * pageSize;
            if (skip<0) {
                skip = 0;
            }
            return query
                .OrderBy(p=>p.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToList();
        }

        IQueryable<Certificate> BuildQuery(CertificateStatus status,string specialtyId) {
            var query = Context.Certificates.AsQueryable();
            if (!string.IsNullOrWhiteSpace(specialtyId)) {
                query = query.Where(p => p.SpecialtyId.Equals(specialtyId));
            }
            DateTime now = DateTime.Now;
            switch (status) {
                case CertificateStatus.All:
                    break;
                case CertificateStatus.Normal:
                    query = query.Where(p => p.ExpireDateTime.CompareTo(now) >= 0);
                    break;
                case CertificateStatus.UpComing:
                    query = query.Where(p => (p.ExpireDateTime - now).Days <= 30);
                    break;
                case CertificateStatus.Expired:
                    query = query.Where(p => p.ExpireDateTime.CompareTo(now) < 0);
                    break;
            }
            return query;
        }

        /// <summary>
        /// 根据证书编号或者用姓名搜索证书
        /// </summary>
        /// <param name="userNameOrNO"></param>
        /// <param name="specialty"></param>
        /// <returns></returns>
        public IList<Certificate> Serach(string userNameOrNO , string specialty = "") {
            var query = Context.Certificates.Where(p => p.EpmloyeeName.Contains(userNameOrNO) || p.Number == userNameOrNO);
            if (!string.IsNullOrWhiteSpace(specialty)) {
                query = query.Where(p => p.SpecialtyId.Equals(specialty));
            }
            return query.ToList();
        }
    }

    public enum CertificateStatus
    {
        All,
        /// <summary>
        /// 正常
        /// </summary>
        Normal ,
        /// <summary>
        /// 即将到期
        /// </summary>
        UpComing ,
        /// <summary>
        /// 已经过期
        /// </summary>
        Expired
    }
}
